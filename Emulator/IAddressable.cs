namespace Gamemu.Emulator
{
    public interface ISource
    {
        int Read();
    }

    public interface IDest
    {
        void Write(int value);
    }

    public interface IAddressable : ISource, IDest {}
}