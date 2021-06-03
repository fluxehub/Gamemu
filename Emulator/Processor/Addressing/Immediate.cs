namespace Gamemu.Emulator.Processor.Addressing
{
    public class Immediate : ISource
    {
        protected readonly MemoryMap Memory;
        protected readonly Register16 PC;

        public Immediate(MemoryMap memoryMap, Register16 pc)
        {
            Memory = memoryMap;
            PC = pc;
        }

        public virtual int Read()
        {
            PC.Increment();
            return Memory.Read(PC.Read());
        }
    }
    
    public class ImmediateSigned : Immediate
    {
        public ImmediateSigned(MemoryMap memoryMap, Register16 pc) : base(memoryMap, pc)
        {
        }

        public override int Read() => (sbyte) base.Read();
    }

    public class Immediate16 : Immediate
    {

        public Immediate16(MemoryMap memoryMap, Register16 pc) : base(memoryMap, pc)
        {
        }
        
        public override int Read()
        {
            PC.Increment();
            var low = Memory.Read(PC.Read());
            PC.Increment();
            var high = Memory.Read(PC.Read());
            return (high << 8) | low;
        }
    }
}