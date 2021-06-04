using System;

namespace Gamemu.Emulator.Processor.Addressing
{
    public class Register : IAddressable
    {
        protected int Value;

        public virtual int Read()
        {
            return Value;
        }

        public virtual void Write(int value)
        {
            Value = value & 0xFF;
        }

        public void Increment()
        {
            Write(Read() + 1);
        }
        
        public void Decrement()
        {
            Write(Read() - 1);
        }
    }

    public class Register16 : Register
    {
        public override void Write(int value)
        {
            Value = value & 0xFFFF;
        }
    }

    public class FlagsRegister : Register
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

        // Needed for the POP instruction
        public override void Write(int value)
        {
            ZeroFlag = ((value >> 7) & 1) == 1;
            SubtractionFlag = ((value >> 6) & 1) == 1;
            HalfCarryFlag = ((value >> 5) & 1) == 1;
            CarryFlag = ((value >> 4) & 1) == 1;
        }
    }

    public class CombinedRegister : Register
    {
        private readonly Register _high;
        private readonly Register _low;

        public CombinedRegister(Register high, Register low)
        {
            _high = high;
            _low = low;
        }

        public override int Read()
        {
            return (_high.Read() << 8) | _low.Read();
        }

        public override void Write(int value)
        {
            _high.Write((value >> 8) & 0xFF);
            _low.Write(value & 0xFF);
        }
    }
}