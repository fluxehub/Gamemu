namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0x00)]
    public class NOP : Instruction
    {
        public NOP(CPU cpu, int cycles) : base(cpu, cycles)
        {
        }

        // Incredible
        public override void Execute()
        {
        }
    }
}