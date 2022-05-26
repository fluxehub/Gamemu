namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0xFB)]
public class Ei : Instruction
{
    private readonly Cpu _cpu;
        
    public Ei(Cpu cpu, int cycles) : base(cycles)
    {
        _cpu = cpu;
    }

    public override void Execute()
    {
        // Interrupts are enabled on the next cycle
        _cpu.InterruptStatus = InterruptStatus.Pending;
    }
}