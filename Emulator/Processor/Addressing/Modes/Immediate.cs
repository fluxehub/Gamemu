namespace Gamemu.Emulator.Processor.Addressing.Modes;

public class Immediate : ISource
{
    protected readonly MemoryMap Memory;
    protected readonly Register16 Pc;

    public Immediate(MemoryMap memoryMap, Register16 pc)
    {
        Memory = memoryMap;
        Pc = pc;
    }

    public virtual int Read()
    {
        Pc.Increment();
        return Memory[Pc.Read()];
    }
}
    
// This doesn't really need to exist, but it might be helpful for debugging output
public class Immediate8Signed : Immediate
{
    public Immediate8Signed(MemoryMap memoryMap, Register16 pc) : base(memoryMap, pc)
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
        Pc.Increment();
        var low = Memory[Pc.Read()];
        Pc.Increment();
        var high = Memory[Pc.Read()];
        return (high << 8) | low;
    }
}