using System;

namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0x10)]
    public class STOP : Instruction
    {
        private readonly CPU _cpu;
        
        public STOP(CPU cpu, int cycles) : base(cycles)
        {
            _cpu = cpu;
        }

        public override void Execute()
        {
            _cpu.Status = CPUStatus.Stopped;
            Console.WriteLine("STOP called, stopping CPU");
        }
    }
}