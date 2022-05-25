using Gamemu.Emulator.Processor.Addressing.Modes;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0xB8, Source = RegisterB)]
[Instruction(Opcode = 0xB9, Source = RegisterC)]
[Instruction(Opcode = 0xBA, Source = RegisterD)]
[Instruction(Opcode = 0xBB, Source = RegisterE)]
[Instruction(Opcode = 0xBC, Source = RegisterH)]
[Instruction(Opcode = 0xBD, Source = RegisterL)]
[Instruction(Opcode = 0xBE, Cycles = 8, Source = AbsoluteHl)]
[Instruction(Opcode = 0xBF, Source = RegisterA)]
    
[Instruction(Opcode = 0xFE, Cycles = 8, Source = ImmediateValue8)]
public class Cp : ReadInstruction
{
    private readonly Register _a;
    private readonly FlagsRegister _flags;

    public Cp(ISource source, [A] Register a, FlagsRegister flags, int cycles) : base(source, cycles)
    {
        _a = a;
        _flags = flags;
    }

    public override void Execute()
    {
        var a = _a.Read();
        var b = -Source.Read();
        
        InstructionUtilities.SetFlags(_flags, a, b, true);
    }
}