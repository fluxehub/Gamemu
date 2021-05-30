namespace Gamemu.Emulator
{
    public class MBC2 : Cartridge
    {
        private bool _hasBattery;

        public MBC2(byte[] data, bool hasBattery) : base(data) {
            _hasBattery = hasBattery;
        }
    }
}