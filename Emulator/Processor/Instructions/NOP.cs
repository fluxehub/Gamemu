namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0x00)]
    public class NOP : Instruction
    {
        public NOP(int cycles) : base(cycles)
        {
        }

        // Incredible
        public override void Execute()
        {
        }
    }
}