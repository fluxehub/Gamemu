using System;

namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0x76)]
    public class HALT : Instruction
    {
        private readonly CPU _cpu;
        
        public HALT(CPU cpu, int cycles) : base(cycles)
        {
            _cpu = cpu;
        }

        public override void Execute()
        {
            _cpu.Status = CPUStatus.Halted;
        }
    }
}