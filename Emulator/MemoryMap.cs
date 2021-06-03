using System;

namespace Gamemu.Emulator
{
    public class RAM : IMemory
    {
        private readonly byte[] _ram;

        public RAM(int size)
        {
            _ram = new byte[size];
        }

        public int Read(int address)
        {
            return _ram[address];
        }

        public void Write(int address, int value)
        {
            _ram[address] = (byte) value;
        }
    }

    public class MemoryMap
    {
        private readonly Cartridge _cartridge;
        private RAM _wram = new(8192);
        private RAM _hram = new(128);

        public MemoryMap(Cartridge cartridge)
        {
            _cartridge = cartridge;
        }

        // TODO: Work out how to return address with offset removed
        private IMemory GetMemoryDevice(int address) => address switch
        {
            <      0 => throw new ArgumentOutOfRangeException($"Attempted to access invalid memory address 0x{address:X4}"),
            < 0x8000 => _cartridge,
            < 0xA000 => throw new ArgumentException($"Attempted to access VRAM address 0x${address:X4}"),
            < 0xC000 => _cartridge,
            _ => throw new ArgumentOutOfRangeException($"Attempted to access invalid memory location 0x{address:X4}")
        };

        public int Read(int address) => GetMemoryDevice(address).Read(address);

        public void Write(int address, int value) => GetMemoryDevice(address).Write(address, value);
    }
}