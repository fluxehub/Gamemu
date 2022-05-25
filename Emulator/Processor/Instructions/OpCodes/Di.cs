namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0xF3)]
public class Di : Instruction
{
    private readonly CPU _cpu;
        
    public Di(CPU cpu, int cycles) : base(cycles)
    {
        _cpu = cpu;
    }

    public override void Execute()
    {
        _cpu.InterruptStatus = InterruptStatus.Disabled;
    }
}