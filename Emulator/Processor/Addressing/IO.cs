namespace Gamemu.Emulator.Processor.Addressing
{
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
            return _memory.Read(0xFF00 + _source.Read());
        }
        
        public void Write(int value)
        {
            _memory.Write(0xFF00 + _source.Read(), value);
        }
    }
}