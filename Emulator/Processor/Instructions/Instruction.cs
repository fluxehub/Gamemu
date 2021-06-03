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

    public abstract class SameRegisterInstruction : Instruction
    {
        protected readonly Register Register;

        protected SameRegisterInstruction(Register register, int cycles) : base(cycles)
        {
            Register = register;
        }
    }
}