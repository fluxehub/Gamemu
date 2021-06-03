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
            AddressingMode.RegisterSP => StackPointer,
            AddressingMode.AbsoluteBC => new Absolute(Memory, _bc),
            AddressingMode.AbsoluteDE => new Absolute(Memory, _de),
            AddressingMode.AbsoluteHL => new Absolute(Memory, _hl),
            AddressingMode.AbsoluteSP => new Absolute(Memory, StackPointer),
            AddressingMode.AbsoluteHLInc => new AbsoluteWithRegIncOrDec(Memory, _hl, 1),
            AddressingMode.AbsoluteHLDec => new AbsoluteWithRegIncOrDec(Memory, _hl, -1),
            _ => null
        };

        private void CreateInstructionTable()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                var instructionAttributes = type.GetCustomAttributes(typeof(InstructionAttribute))
                    .Select(a => (InstructionAttribute) a)
                    .ToList();

                if (instructionAttributes.Count <= 0) continue;
                
                foreach (var attribute in instructionAttributes)
                {
                    var ctor = type.GetConstructors()[0];
                    var opcode = attribute.Opcode;
                    var cycles = attribute.Cycles;
                    
                    var isCB = (opcode >> 2) == 0xCB;
                    
                    var table = isCB ? _cbInstructionTable : _instructionTable;
                    if (table[opcode] != null)
                    {
                        var cbString = isCB ? "CB" : "";
                        throw new ArgumentException($"Opcode collision at 0x{cbString}{opcode:X2}");
                    }

                    opcode = isCB ? opcode >> 2 : opcode;
                    
                    // The first 2 to 3 parameters will always be the CPU, the cycle count
                    // and optionally an alternate cycle count
                    var parameters = new List<object>() {this, cycles};

                    if (attribute.CyclesAlternate != 0)
                    {
                        parameters.Add(attribute.CyclesAlternate);
                    }
                    
                    // Add the rest of the parameters
                    foreach (var param in ctor.GetParameters().Skip(parameters.Count))
                    {
                        // TODO: Work out how to switch on type not name of type
                        switch (param.ParameterType.Name)
                        {
                            case nameof(ISource):
                                parameters.Add(GetParameterForAddressingMode(attribute.Source));
                                break;
                            case nameof(IDest):
                                parameters.Add(GetParameterForAddressingMode(attribute.Dest));
                                break;
                            case nameof(MemoryMap):
                                parameters.Add(Memory);
                                break;
                            default:
                                throw new ArgumentException(
                                    $"Unknown mapping for parameter \"{param.ParameterType.Name} {param.Name}\"");
                        }
                    }
                    
                    if (parameters.Count == ctor.GetParameters().Length)
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