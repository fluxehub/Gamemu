using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Gamemu.Emulator.Processor.Instructions;
using Gamemu.Emulator.Processor.Addressing;
using Gamemu.Emulator.Processor.Addressing.Modes;

namespace Gamemu.Emulator.Processor;

public partial class CPU
{
    private readonly Instruction[] _instructionTable = new Instruction[256];
    private readonly Instruction[] _cbInstructionTable = new Instruction[256];

    private object GetParameterForAddressingMode(AddressingMode mode) => mode switch
    {
        AddressingMode.RegisterA => _a,
        AddressingMode.RegisterB => _b,
        AddressingMode.RegisterC => _c,
        AddressingMode.RegisterD => _d,
        AddressingMode.RegisterE => _e,
        AddressingMode.RegisterH => _h,
        AddressingMode.RegisterL => _l,
        AddressingMode.RegisterAf => _af,
        AddressingMode.RegisterBc => _bc,
        AddressingMode.RegisterDe => _de,
        AddressingMode.RegisterHl => _hl,
        AddressingMode.RegisterSp => _sp,
        AddressingMode.AbsoluteBc => new Absolute(_memory, _bc),
        AddressingMode.AbsoluteDe => new Absolute(_memory, _de),
        AddressingMode.AbsoluteHl => new Absolute(_memory, _hl),
        AddressingMode.AbsoluteSp => new Absolute(_memory, _sp),
        AddressingMode.AbsoluteHlInc => new AbsoluteWithRegIncOrDec(_memory, _hl, 1),
        AddressingMode.AbsoluteHlDec => new AbsoluteWithRegIncOrDec(_memory, _hl, -1),
        AddressingMode.AbsoluteImmediate => new Absolute(_memory, new Immediate16(_memory, _pc)),
        AddressingMode.ImmediateValue8 => new Immediate(_memory, _pc),
        AddressingMode.ImmediateValue8Signed => new Immediate8Signed(_memory, _pc),
        AddressingMode.ImmediateValue16 => new Immediate16(_memory, _pc),
        AddressingMode.IOImmediate => new IO(_memory, new Immediate16(_memory, _pc)),
        AddressingMode.IORegisterC => new IO(_memory, _c),
        _ => throw new ArgumentException($"Addressing mode {mode.ToString()} not implemented")
    };

    /// <summary>
    /// Generates the unprefixed and 0xCB-prefixed instruction tables, where each index is a reference to a created
    /// instruction object.
    /// </summary>
    /// <exception cref="ArgumentException">If unknown object parameters are defined in an instruction or unknown attributes are used</exception>
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
                    
                var isCb = (opcode >> 2) == 0xCB;
                    
                var table = isCb ? _cbInstructionTable : _instructionTable;
                if (table[opcode] != null)
                {
                    var cbString = isCb ? "CB" : "";
                    throw new ArgumentException($"Opcode collision at 0x{cbString}{opcode:X2}");
                }

                opcode = isCb ? opcode >> 2 : opcode;
                    
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
                                    
                            // There is only ever one attribute
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
                    else if (instructionAttributeType == typeof(IAddressable))
                    {
                        parameters.Add(GetParameterForAddressingMode(instructionAttribute.Addressable));
                    }
                    else if (instructionAttributeType == typeof(Register))
                    {
                        var paramAttributes = param.GetCustomAttributes().ToList();
                            
                        // The only register that can be passed is A
                        if (paramAttributes.Count == 0 || paramAttributes[0].GetType() != typeof(AAttribute))
                            throw new ArgumentException(
                                $"Unknown mapping for parameter \"{instructionAttributeType.Name} {param.Name}\"");

                        parameters.Add(_a);
                    }
                    else if (instructionAttributeType == typeof(Register16))
                    {
                        var paramAttributes = param.GetCustomAttributes().ToList();

                        if (paramAttributes.Count == 0)
                        {
                            throw new ArgumentException(
                                $"Unknown mapping for parameter \"{instructionAttributeType.Name} {param.Name}\"");
                        }
                            
                        // There is only ever one attribute
                        var attributeType = paramAttributes[0].GetType();
                                
                        if (attributeType == typeof(SpAttribute))
                        {
                            parameters.Add(_sp);
                        } 
                        else if (attributeType == typeof(PcAttribute))
                        {
                            parameters.Add(_pc);
                        }
                    }
                    else if (instructionAttributeType == typeof(MemoryMap))
                    {
                        parameters.Add(_memory);
                    }
                    else if (instructionAttributeType == typeof(FlagsRegister))
                    {
                        parameters.Add(_f);
                    }
                    else if (instructionAttributeType == typeof(CPU))
                    {
                        parameters.Add(this);
                    }
                    else if (instructionAttributeType == typeof(Condition))
                    {
                        Condition condition = instructionAttribute.JumpCondition switch
                        {
                            ConditionType.Zero => new ZeroCondition(_f),
                            ConditionType.NotZero => new NotZeroCondition(_f),
                            ConditionType.Carry => new CarryCondition(_f),
                            ConditionType.NotCarry => new NotCarryCondition(_f),
                            _ => null
                        };

                        parameters.Add(condition);
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
            0xCB, 0xD3, 0xE3, 0xE4, 
            0xF4, 0xDB, 0xEB, 0xEC, 
            0xFC, 0xDD, 0xED, 0xFD
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