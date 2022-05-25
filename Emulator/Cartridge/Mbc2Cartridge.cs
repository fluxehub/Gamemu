namespace Gamemu.Emulator.Cartridge;

public class Mbc2Cartridge : BaseCartridge
{
    private bool _hasBattery;

    public Mbc2Cartridge(byte[] data, bool hasBattery) : base(data) {
        _hasBattery = hasBattery;
    }
    
    protected override int Read(int address)
    {
        throw new System.NotImplementedException();
    }

    protected override void Write(int address, int value)
    {
        throw new System.NotImplementedException();
    }
}