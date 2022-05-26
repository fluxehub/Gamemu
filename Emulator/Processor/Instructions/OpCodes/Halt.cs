namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0x76)]
public class Halt : Instruction
{
    private readonly Cpu _cpu;
        
    public Halt(Cpu cpu, int cycles) : base(cycles)
    {
        _cpu = cpu;
    }

    public override void Execute()
    {
        _cpu.ExecutionStatus = ExecutionStatus.Halted;
    }
}