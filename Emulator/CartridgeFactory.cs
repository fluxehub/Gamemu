using System;
using System.IO;

namespace Gamemu.Emulator
{
    public class CartridgeFactory
    {
        private readonly byte[] _data;

        public CartridgeFactory(string romFile)
        {
            _data = File.ReadAllBytes(romFile);
        }

        public Cartridge MakeCartridge() =>
            _data[0x0147] switch
            {
                // ROM only
                0x00 => new Cartridge(_data),
                // MBC1
                0x01 => new MBC1(_data, false, false),
                // MBC1 + RAM
                0x02 => new MBC1(_data, true, false),
                // MBC1 + RAM + Battery
                0x03 => new MBC1(_data, true, true),
                // MBC2
                0x05 => new MBC2(_data, false),
                // MBC2 + Battery
                0x06 => new MBC2(_data, true),
                // MBC3 + RTC
                0x0F => new MBC3(_data, false, false, true),
                // MBC3 + RAM + Battery + RTC
                0x10 => new MBC3(_data, true, true, true),
                // MBC3
                0x11 => new MBC3(_data, false, false, false),
                // MBC3 + RAM
                0x12 => new MBC3(_data, true, false, false),
                // MBC3 + RAM + Battery
                0x13 => new MBC3(_data, true, true, false),
                _ => throw new Exception($"Cartridge type not supported: {_data[0x0147]}")
            };
    }
}