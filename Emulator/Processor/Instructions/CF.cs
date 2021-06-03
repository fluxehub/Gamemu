using System.Diagnostics.CodeAnalysis;

namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0x37)]
    public class SCF : Instruction
    {
        public SCF(CPU cpu, int cycles) : base(cpu, cycles)
        {
        }

        public override void Execute()
        {
            CPU.Flags.SubtractionFlag = false;
            CPU.Flags.HalfCarryFlag = false;
            CPU.Flags.CarryFlag = true;
        }
    }
    
    [Instruction(Opcode = 0x3F)]
    public class CCF : Instruction
    {
        public CCF(CPU cpu, int cycles) : base(cpu, cycles)
        {
        }

        public override void Execute()
        {
            CPU.Flags.SubtractionFlag = false;
            CPU.Flags.HalfCarryFlag = false;
            CPU.Flags.CarryFlag = !CPU.Flags.CarryFlag;
        }
    }
}