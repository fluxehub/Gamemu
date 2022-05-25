using Gamemu.Emulator.Processor.Addressing;
using Gamemu.Emulator.Processor.Addressing.Modes;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0x90, Source = RegisterB)]
[Instruction(Opcode = 0x91, Source = RegisterC)]
[Instruction(Opcode = 0x92, Source = RegisterD)]
[Instruction(Opcode = 0x93, Source = RegisterE)]
[Instruction(Opcode = 0x94, Source = RegisterH)]
[Instruction(Opcode = 0x95, Source = RegisterL)]
[Instruction(Opcode = 0x96, Cycles = 8, Source = AbsoluteHl)]
[Instruction(Opcode = 0x97, Source = RegisterA)]
    
[Instruction(Opcode = 0xD6, Cycles = 8, Source = AddressingMode.ImmediateValue8)]
public class Sub : ReadInstruction
{
    private readonly Register _a;
    private readonly FlagsRegister _flags;

    public Sub(ISource source, [A] Register a, FlagsRegister flags, int cycles) : base(source, cycles)
    {
        _a = a;
        _flags = flags;
    }

    public override void Execute()
    {
        var a = _a.Read();
        var b = -Source.Read();
            
        _a.Write(a + b);
        InstructionUtilities.SetFlags(_flags, a, b, true);
    }
}