using Gamemu.Emulator.Processor.Addressing;

namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0x2F)]
    public class CPL : Instruction
    {
        private readonly Register _a;
        private readonly FlagsRegister _flags;

        public CPL([A] Register a, FlagsRegister flags, int cycles) : base(cycles)
        {
            _a = a;
            _flags = flags;
        }

        public override void Execute()
        {
            _a.Write(~_a.Read());
            _flags.SubtractionFlag = true;
            _flags.HalfCarryFlag = true;
        }
    }
}