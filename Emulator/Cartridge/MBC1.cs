namespace Gamemu.Emulator
{
    public class MBC1 : Cartridge
    {
        private bool _hasRAM;
        private bool _hasBattery;

        public MBC1(byte[] data, bool hasRAM, bool hasBattery) : base(data) {
            _hasRAM = hasRAM;
            _hasBattery = hasBattery;
        }
    }
}