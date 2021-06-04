namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0xF3)]
    public class DI : Instruction
    {
        private readonly CPU _cpu;
        
        public DI(CPU cpu, int cycles) : base(cycles)
        {
            _cpu = cpu;
        }

        public override void Execute()
        {
            _cpu.InterruptStatus = InterruptStatus.Disabled;
        }
    }
}