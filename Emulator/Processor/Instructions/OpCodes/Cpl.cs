using Gamemu.Emulator.Processor.Addressing.Modes;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0x2F)]
public class Cpl : Instruction
{
    private readonly Register _a;
    private readonly FlagsRegister _flags;

    public Cpl([A] Register a, FlagsRegister flags, int cycles) : base(cycles)
    {
        _a = a;
        _flags = flags;
    }

    public override void Execute()
    {
        _a.Write(~_a.Read());
        _flags.SubtractionFlag = true;
        _flags.HalfCarryFlag = true;
    }
}