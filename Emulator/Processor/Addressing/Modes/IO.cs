namespace Gamemu.Emulator.Processor.Addressing.Modes;

public class IO : IAddressable
{
    private readonly MemoryMap _memory;
    private readonly ISource _source;

    public IO(MemoryMap memory, ISource source)
    {
        _memory = memory;
        _source = source;
    }

    public int Read()
    {
        return _memory[0xFF00 + _source.Read()];
    }
        
    public void Write(int value)
    {
        _memory[0xFF00 + _source.Read()] = value;
    }
}