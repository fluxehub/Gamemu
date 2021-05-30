using System;

namespace Gamemu.Emulator
{
    public class MemoryBus : IAddressable
    {
        public int Address { 
            get => _address;
            set {
                if (_address > 0xFFFF || _address < 0)
                    throw new ArgumentException($"Attempted to use invalid memory address {value}");
                
                _address = value & 0xFFFF;
            }
        }

        private int _address;
        private MemoryMap _memoryMap;

        public MemoryBus(MemoryMap memoryMap) 
        {
            _memoryMap = memoryMap;
        }

        public int Read()
        {
            return _memoryMap.Read(_address);
        }

        public void Write(int value)
        {
            _memoryMap.Write(_address, value);
        }

        public int ReadAddress(int address)
        {
            Address = address;
            return Read();
        }

        public void WriteAddress(int address, int value)
        {
            Address = address;
            Write(value);
        }
    }
}