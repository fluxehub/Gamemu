namespace Gamemu.Emulator.Processor.Addressing
{
    public class Absolute : IAddressable
    {
        private readonly ISource _source;
        private readonly MemoryMap _memoryMap;

        public Absolute(MemoryMap map, ISource source)
        {
            _source = source;
            _memoryMap = map;
        }

        public int Read()
        {
            return _memoryMap[_source.Read()];
        }

        public void Write(int value)
        {
            _memoryMap[_source.Read()] = value;
        }
    }

    // Used for (HL+)/(HL-) instructions
    public class AbsoluteWithRegIncOrDec : IAddressable
    {
        private readonly CombinedRegister _register;
        private readonly MemoryMap _memoryMap;
        private readonly int _shift;

        public AbsoluteWithRegIncOrDec(MemoryMap map, CombinedRegister register, int shift)
        {
            _register = register;
            _memoryMap = map;
            _shift = shift;
        }

        public int Read()
        {
            var regValue = _register.Read();
            var value = _memoryMap[regValue];
            _register.Write(regValue + _shift);
            return value;
        }

        public void Write(int value)
        {
            var regValue = _register.Read();
            _memoryMap[regValue] = value;
            _register.Write(regValue + _shift);
        }
    }
}