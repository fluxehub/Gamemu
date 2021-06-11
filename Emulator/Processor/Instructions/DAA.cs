using Gamemu.Emulator.Processor.Addressing;

namespace Gamemu.Emulator.Processor.Instructions
{
    // God help me
    [Instruction(Opcode = 0x27)]
    public class DAA : Instruction
    {
        private readonly Register _a;
        private readonly FlagsRegister _flags;
        
        public DAA([A] Register a, FlagsRegister flags, int cycles) : base(cycles)
        {
            _a = a;
            _flags = flags;
        }
        
        // It is too late
        // https://www.reddit.com/r/EmuDev/comments/4ycoix/a_guide_to_the_gameboys_halfcarry_flag/d6p3rtl
        public override void Execute()
        {
            var value = _a.Read();
            var adjustment = 0;

            if (_flags.CarryFlag || (!_flags.SubtractionFlag && value > 0x99))
            {
                adjustment = 0x60;
                _flags.CarryFlag = true;
            }

            if (_flags.HalfCarryFlag || (!_flags.SubtractionFlag && (value & 0xF) > 9))
                adjustment |= 0x06;

            if (_flags.SubtractionFlag)
                value -= adjustment;
            else
                value += adjustment;

            _flags.ZeroFlag = value == 0;
            _flags.HalfCarryFlag = false;
            _a.Write(value);
        }
    }
}