namespace Gamemu.Emulator
{
    public class MBC3 : Emulator.Cartridge
    {
        private bool _hasRAM;
        private bool _hasBattery;
        private bool _hasRTC;

        public MBC3(byte[] data, bool hasRAM, bool hasBattery, bool hasRTC) : base(data) {
            _hasRAM = hasRAM;
            _hasBattery = hasBattery;
            _hasRTC = hasRTC;
        }
    }
}