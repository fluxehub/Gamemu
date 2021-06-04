using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Gamemu.Emulator.Processor.Instructions;
using Gamemu.Emulator.Processor.Addressing;

namespace Gamemu.Emulator.Processor
{
    public partial class CPU
    {
        private Instruction[] _instructionTable = new Instruction[0xFF];
        private Instruction[] _cbInstructionTable = new Instruction[0xFF];

        private object GetParameterForAddressingMode(AddressingMode mode) => mode switch
        {
            AddressingMode.RegisterA => _a,
            AddressingMode.RegisterB => _b,
            AddressingMode.RegisterC => _c,
            AddressingMode.RegisterD => _d,
            AddressingMode.RegisterE => _e,
            AddressingMode.RegisterH => _h,
            AddressingMode.RegisterL => _l,
            AddressingMode.RegisterAF => _af,
            AddressingMode.RegisterBC => _bc,
            AddressingMode.RegisterDE => _de,
            AddressingMode.RegisterHL => _hl,
            AddressingMode.RegisterSP => _sp,
            AddressingMode.AbsoluteBC => new Absolute(_memory, _bc),
            AddressingMode.AbsoluteDE => new Absolute(_memory, _de),
            AddressingMode.AbsoluteHL => new Absolute(_memory, _hl),
            AddressingMode.AbsoluteSP => new Absolute(_memory, _sp),
            AddressingMode.AbsoluteHLInc => new AbsoluteWithRegIncOrDec(_memory, _hl, 1),
            AddressingMode.AbsoluteHLDec => new AbsoluteWithRegIncOrDec(_memory, _hl, -1),
            AddressingMode.AbsoluteImmediate => new Absolute(_memory, new Immediate16(_memory, _pc)),
            AddressingMode.Immediate => new Immediate(_memory, _pc),
            AddressingMode.ImmediateSigned => new ImmediateSigned(_memory, _pc),
            AddressingMode.Immediate16 => new Immediate16(_memory, _pc),
            AddressingMode.IOImmediate => new IO(_memory, new Immediate16(_memory, _pc)),
            AddressingMode.IORegisterC => new IO(_memory, _c),
            _ => throw new ArgumentException($"Addressing mode {mode.ToString()} not implemented")
        };

        private void CreateInstructionTable()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                var instructionAttributes = type.GetCustomAttributes(typeof(InstructionAttribute))
                    .Select(a => (InstructionAttribute) a)
                    .ToList();

                if (instructionAttributes.Count <= 0) continue;
                
                foreach (var instructionAttribute in instructionAttributes)
                {
                    var ctor = type.GetConstructors()[0];
                    var opcode = instructionAttribute.Opcode;
                    
                    var isCB = (opcode >> 2) == 0xCB;
                    
                    var table = isCB ? _cbInstructionTable : _instructionTable;
                    if (table[opcode] != null)
                    {
                        var cbString = isCB ? "CB" : "";
                        throw new ArgumentException($"Opcode collision at 0x{cbString}{opcode:X2}");
                    }

                    opcode = isCB ? opcode >> 2 : opcode;
                    
                    var parameters = new List<object>(ctor.GetParameters().Length);
                    
                    foreach (var param in ctor.GetParameters())
                    {
                        var instructionAttributeType = param.ParameterType;

                        if (instructionAttributeType == typeof(int))
                        {
                            var paramAttributes = param.GetCustomAttributes().ToList();

                            if (paramAttributes.Count > 0)
                            {
                                var attributeType = paramAttributes[0].GetType();
                                    
                                // There's only one parameter attribute
                                if (attributeType == typeof(AlternateAttribute))
                                {
                                    parameters.Add(instructionAttribute.CyclesAlternate);
                                } 
                                else if (attributeType == typeof(RestartAddressAttribute))
                                {
                                    parameters.Add(instructionAttribute.RestartAddress);
                                }
                            }
                            else
                            {
                                parameters.Add(instructionAttribute.Cycles);
                            }
                        } 
                        else if (instructionAttributeType == typeof(ISource))
                        {
                            parameters.Add(GetParameterForAddressingMode(instructionAttribute.Source));
                        } 
                        else if (instructionAttributeType == typeof(IDest))
                        {
                            parameters.Add(GetParameterForAddressingMode(instructionAttribute.Dest));
                        }  
                        else if (instructionAttributeType == typeof(Register))
                        {
                            parameters.Add(GetParameterForAddressingMode(instructionAttribute.Addressable));
                        } 
                        else if (instructionAttributeType == typeof(MemoryMap))
                        {
                            parameters.Add(_memory);
                        }
                        else if (instructionAttributeType == typeof(FlagsRegister))
                        {
                            parameters.Add(_f);
                        }
                        else
                        {
                            throw new ArgumentException(
                                $"Unknown mapping for parameter \"{instructionAttributeType.Name} {param.Name}\"");
                        }
                    }
                    
                    
                   // if (parameters.Count == ctor.GetParameters().Length)
                   table[opcode] = (Instruction) ctor.Invoke(parameters.ToArray());
                }
            }

            var invalidOpcodes = new List<int>()
            {
                0xD3, 0xE3, 0xE4, 0xF4,
                0xDB, 0xEB, 0xEC, 0xFC,
                0xDD, 0xED, 0xFD
            };

            for (var i = 0; i < 0xFF; i++)
            {
                if (invalidOpcodes.Contains(i)) continue;
                    
                if (_instructionTable[i] == null)
                {
                    Console.WriteLine($"No instruction given for opcode 0x{i:X2}");
                }
            }
        }
    }
}