namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0x76)]
public class Halt : Instruction
{
    private readonly CPU _cpu;
        
    public Halt(CPU cpu, int cycles) : base(cycles)
    {
        _cpu = cpu;
    }

    public override void Execute()
    {
        _cpu.ExecutionStatus = ExecutionStatus.Halted;
    }
}