using Gamemu.Emulator.Processor.Addressing;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions
{
    public abstract class Instruction
    {
        public int Cycles { get; private set; }

        protected Instruction(int cycles)
        {
            Cycles = cycles;
        }

        public abstract void Execute();
    }
    
    // For instructions that only read and use a side effect
    // e.g. PUSH
    public abstract class ReadInstruction : Instruction
    {
        protected readonly ISource Source;

        protected ReadInstruction(ISource source, int cycles) : base(cycles)
        {
            Source = source;
        }
    }

    public abstract class WriteInstruction : Instruction
    {
        protected readonly IDest Dest;
        
        protected WriteInstruction(IDest dest, int cycles) : base(cycles)
        {
            Dest = dest;
        }
    }

    public abstract class ReadWriteInstruction : WriteInstruction
    {
        protected readonly ISource Source;
        
        protected ReadWriteInstruction(ISource source, IDest dest, int cycles) : base(dest, cycles)
        {
            Source = source;
        }
    }

    // Instructions that read and write to the same place, such as INC
    public abstract class SameReadWriteInstruction : Instruction
    {
        protected readonly IAddressable Addressable;

        protected SameReadWriteInstruction(IAddressable addressable, int cycles) : base(cycles)
        {
            Addressable = addressable;
        }
    }
}