namespace Gamemu.Emulator.Cartridge;

public class Mbc3Cartridge : BaseCartridge
{
    private bool _hasRam;
    private bool _hasBattery;
    private bool _hasRtc;

    public Mbc3Cartridge(byte[] data, bool hasRam, bool hasBattery, bool hasRtc) : base(data) {
        _hasRam = hasRam;
        _hasBattery = hasBattery;
        _hasRtc = hasRtc;
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