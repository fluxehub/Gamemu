using Gamemu.Emulator.Processor.Addressing;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions
{
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = true)]  
    public class InstructionAttribute : System.Attribute
    {
        public int Opcode;
        public int Cycles = 4;
        public int CyclesAlternate = 0;
        public AddressingMode Source = None;
        public AddressingMode Dest = None;
    }
    
    public abstract class Instruction
    {
        protected readonly CPU CPU;
        protected readonly int CyclesAlternate = 0;
        
        public int Cycles { get; private set; }

        protected Instruction(CPU cpu, int cycles)
        {
            CPU = cpu;
            Cycles = cycles;
        }

        public abstract void Execute();
    }

    public abstract class WriteInstruction : Instruction
    {
        protected readonly IDest Dest;
        
        protected WriteInstruction(CPU cpu, int cycles, IDest dest) : base(cpu, cycles)
        {
            Dest = dest;
        }
    }

    public abstract class ReadWriteInstruction : WriteInstruction
    {
        protected readonly ISource Source;
        
        protected ReadWriteInstruction(CPU cpu, int cycles, ISource source, IDest dest) : base(cpu, cycles, dest)
        {
            Source = source;
        }
    }
}