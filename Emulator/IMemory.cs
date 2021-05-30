namespace Gamemu.Emulator
{
    public interface IMemory
    {
        int Read(int address);

        void Write(int address, int value);
    }
}