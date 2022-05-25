using Gamemu.Emulator.Processor.Addressing;
using Gamemu.Emulator.Processor.Addressing.Modes;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

public class Rlca : Instruction
{
    private readonly Register _a;

    public Rlca([A] Register a, int cycles) : base(cycles)
    {
        _a = a;
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }
}