using Gamemu.Emulator.Processor.Addressing;

namespace Gamemu.Emulator.Processor.Instructions
{
    public class RLCA : Instruction
    {
        private readonly Register _a;

        public RLCA([A] Register a, int cycles) : base(cycles)
        {
            _a = a;
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}