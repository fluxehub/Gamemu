namespace Gamemu.Emulator
{
    public abstract class Memory
    {
        protected abstract int Read(int address);

        protected abstract void Write(int address, int value);

        public int this[int address]
        {
            get => Read(address);
            set => Write(address, value);
        }
    }
}