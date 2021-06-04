namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0xFB)]
    public class EI : Instruction
    {
        private readonly CPU _cpu;
        
        public EI(CPU cpu, int cycles) : base(cycles)
        {
            _cpu = cpu;
        }

        public override void Execute()
        {
            // Interrupts are enabled on the next cycle
            _cpu.InterruptStatus = InterruptStatus.Pending;
        }
    }
}