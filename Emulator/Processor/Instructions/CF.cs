using System.Diagnostics.CodeAnalysis;
using Gamemu.Emulator.Processor.Addressing;

namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0x37)]
    public class SCF : Instruction
    {
        private readonly FlagsRegister _flags;
        
        public SCF(FlagsRegister flags, int cycles) : base(cycles)
        {
            _flags = flags;
        }

        public override void Execute()
        {
            _flags.SubtractionFlag = false;
            _flags.HalfCarryFlag = false;
            _flags.CarryFlag = true;
        }
    }
    
    [Instruction(Opcode = 0x3F)]
    public class CCF : Instruction
    {
        private readonly FlagsRegister _flags;
        
        public CCF(FlagsRegister flags, int cycles) : base(cycles)
        {
            _flags = flags;
        }

        public override void Execute()
        {
            _flags.SubtractionFlag = false;
            _flags.HalfCarryFlag = false;
            _flags.CarryFlag = !_flags.CarryFlag;
        }
    }
}