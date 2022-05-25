using Gamemu.Emulator.Processor.Addressing.Modes;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0x37)]
public class Scf : Instruction
{
    private readonly FlagsRegister _flags;
        
    public Scf(FlagsRegister flags, int cycles) : base(cycles)
    {
        _flags = flags;
    }

    public override void Execute()
    {
        _flags.SubtractionFlag = false;
        _flags.HalfCarryFlag = false;
        _flags.CarryFlag = true;
    }
}
    
[Instruction(Opcode = 0x3F)]
public class Ccf : Instruction
{
    private readonly FlagsRegister _flags;
        
    public Ccf(FlagsRegister flags, int cycles) : base(cycles)
    {
        _flags = flags;
    }

    public override void Execute()
    {
        _flags.SubtractionFlag = false;
        _flags.HalfCarryFlag = false;
        _flags.CarryFlag = !_flags.CarryFlag;
    }
}