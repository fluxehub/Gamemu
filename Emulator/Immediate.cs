namespace Gamemu.Emulator
{
    public class Immediate : ISource
    {
        private int _value;

        public Immediate(int value)
        {
            _value = value;
        }

        public int Read()
        {
            return _value;
        }
    }
}