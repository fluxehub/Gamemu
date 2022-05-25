using System;
using System.IO;

namespace Gamemu.Emulator.Cartridge;

public class CartridgeFactory
{
    private readonly byte[] _data;

    public CartridgeFactory(string romFile)
    {
        _data = File.ReadAllBytes(romFile);
    }

    public BaseCartridge MakeCartridge() =>
        _data[0x0147] switch
        {
            // ROM only
            0x00 => new RomOnlyCartridge(_data),
            // MBC1
            0x01 => new Mbc1Cartridge(_data, false, false),
            // MBC1 + RAM
            0x02 => new Mbc1Cartridge(_data, true, false),
            // MBC1 + RAM + Battery
            0x03 => new Mbc1Cartridge(_data, true, true),
            // MBC2
            0x05 => new Mbc2Cartridge(_data, false),
            // MBC2 + Battery
            0x06 => new Mbc2Cartridge(_data, true),
            // MBC3 + RTC
            0x0F => new Mbc3Cartridge(_data, false, false, true),
            // MBC3 + RAM + Battery + RTC
            0x10 => new Mbc3Cartridge(_data, true, true, true),
            // MBC3
            0x11 => new Mbc3Cartridge(_data, false, false, false),
            // MBC3 + RAM
            0x12 => new Mbc3Cartridge(_data, true, false, false),
            // MBC3 + RAM + Battery
            0x13 => new Mbc3Cartridge(_data, true, true, false),
            _ => throw new Exception($"Cartridge type not supported: {_data[0x0147]}")
        };
}