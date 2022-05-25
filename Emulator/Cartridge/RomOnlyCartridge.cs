using System;
using System.Text;

namespace Gamemu.Emulator.Cartridge;

public class RomOnlyCartridge : BaseCartridge
{
    public RomOnlyCartridge(byte[] data) : base(data)
    {
    }

    protected override int Read(int address)
    {
        return _data[address];
    }

    protected override void Write(int address, int value)
    {
        throw new InvalidOperationException("Attempt to write to ROM-only cartridge");
    }
}