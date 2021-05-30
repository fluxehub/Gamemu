using System;

namespace Gamemu.Emulator
{
    public class CPURegister : IAddressable
    {
        protected int _value;

        public virtual int Read()
        {
            return _value;
        }

        public virtual void Write(int value)
        {
            _value = value & 0xFF;
        }

        public void Increment()
        {
            Write(_value + 1);
        }

        public void Decrement()
        {
            Write(_value - 1);
        }
    }

    public class CPU16Register : CPURegister
    {
        public override void Write(int value)
        {
            _value = value & 0xFFFF;
        }
    }

    public class CPUFlagsRegister : CPURegister
    {
        public bool ZeroFlag { get; set; }
        public bool SubtractionFlag { get; set; }
        public bool HalfCarryFlag { get; set; }
        public bool CarryFlag { get; set; }

        public override int Read()
        {
            var z = ZeroFlag        ? 0b10000000 : 0;
            var s = SubtractionFlag ? 0b01000000 : 0;
            var h = HalfCarryFlag   ? 0b00100000 : 0;
            var c = CarryFlag       ? 0b00010000 : 0;

            return z | s | h | c;
        }

        public override void Write(int value)
        {
            throw new ArgumentException("Attempted to write directly to flag register");
        }
    }

    public class CPUCombinedRegister : IAddressable
    {
        private CPURegister _high;
        private CPURegister _low;

        public CPUCombinedRegister(CPURegister high, CPURegister low)
        {
            _high = high;
            _low = low;
        }

        public int Read()
        {
            return (_high.Read() << 8) | _low.Read();
        }

        public void Write(int value)
        {
            _high.Write((value >> 8) & 0xFF);
            _low.Write(value & 0xFF);
        }
    }
}