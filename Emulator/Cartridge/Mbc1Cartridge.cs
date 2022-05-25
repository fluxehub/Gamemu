namespace Gamemu.Emulator.Cartridge;

public class Mbc1Cartridge : BaseCartridge
{
    private bool _hasRam;
    private bool _hasBattery;
    private bool _ramEnabled = false;

    public Mbc1Cartridge(byte[] data, bool hasRam, bool hasBattery) : base(data) {
        _hasRam = hasRam;
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