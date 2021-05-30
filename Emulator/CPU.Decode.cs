using System;

namespace Gamemu.Emulator.CPU
{
    partial class CPU
    {
        private readonly string[] _r = { "B", "C", "D", "E", "H", "L", "(HL)", "A" };
        private readonly string[] _rp = { "BC", "DE", "HL", "SP" };

        void SetMemoryBusToRegValue(CPUCombinedRegister register)
        {
            _memory.Address = register.Read();
        }

        Immediate GetImmediate16()
        {
            _pc.Increment();
            var high = _memory.ReadAddress(_pc.Read());
            _pc.Increment();
            var low = _memory.ReadAddress(_pc.Read());
            return new Immediate(high << 4 | low);
        }

        // TODO: This method is bad, think of a better way
        int DecodeAndExecute(int opcode)
        {
            switch (opcode)
            {
            case 0x00:
                return 4;
            case 0x01:
                var value = GetImmediate16();
                LD(value, _bc);
                return 12;
            case 0x02:
                SetMemoryBusToRegValue(_bc);
                LD(_memory, _a);
                return 8;
            case 0x03:
                INC16(_bc);
                return 8;
            case 0x37:
                SCF();
                return 4;
#region LD
            case 0x40:
                LD(_b, _b);
                return 4;
            case 0x41:
                LD(_c, _b);
                return 4;
            case 0x42:
                LD(_d, _b);
                return 4;
            case 0x43:
                LD(_e, _b);
                return 4;
            case 0x44:
                LD(_h, _b);
                return 4;
            case 0x45:
                LD(_l, _b);
                return 4;
            case 0x46:
                SetMemoryBusToRegValue(_hl);
                LD(_memory, _b);
                return 8;
            case 0x47:
                LD(_a, _b);
                return 4;
            case 0x48:
                LD(_b, _c);
                return 4;
            case 0x49:
                LD(_c, _c);
                return 4;
            case 0x4A:
                LD(_d, _c);
                return 4;
            case 0x4B:
                LD(_e, _c);
                return 4;
            case 0x4C:
                LD(_h, _c);
                return 4;
            case 0x4D:
                LD(_l, _c);
                return 4;
            case 0x4E:
                SetMemoryBusToRegValue(_hl);
                LD(_memory, _c);
                return 8;
            case 0x4F:
                LD(_a, _c);
                return 4;
            case 0x50:
                LD(_b, _d);
                return 4;
            case 0x51:
                LD(_c, _d);
                return 4;
            case 0x52:
                LD(_d, _d);
                return 4;
            case 0x53:
                LD(_e, _d);
                return 4;
            case 0x54:
                LD(_h, _d);
                return 4;
            case 0x55:
                LD(_l, _d);
                return 4;
            case 0x56:
                SetMemoryBusToRegValue(_hl);
                LD(_memory, _d);
                return 8;
            case 0x57:
                LD(_a, _d);
                return 4;
            case 0x58:
                LD(_b, _e);
                return 4;
            case 0x59:
                LD(_c, _e);
                return 4;
            case 0x5A:
                LD(_d, _e);
                return 4;
            case 0x5B:
                LD(_e, _e);
                return 4;
            case 0x5C:
                LD(_h, _e);
                return 4;
            case 0x5D:
                LD(_l, _e);
                return 4;
            case 0x5E:
                SetMemoryBusToRegValue(_hl);
                LD(_memory, _e);
                return 8;
            case 0x5F:
                LD(_a, _e);
                return 4;
            case 0x60:
                LD(_b, _h);
                return 4;
            case 0x61:
                LD(_c, _h);
                return 4;
            case 0x62:
                LD(_d, _h);
                return 4;
            case 0x63:
                LD(_e, _h);
                return 4;
            case 0x64:
                LD(_h, _h);
                return 4;
            case 0x65:
                LD(_l, _h);
                return 4;
            case 0x66:
                SetMemoryBusToRegValue(_hl);
                LD(_memory, _h);
                return 8;
            case 0x67:
                LD(_a, _h);
                return 4;
            case 0x68:
                LD(_b, _l);
                return 4;
            case 0x69:
                LD(_c, _l);
                return 4;
            case 0x6A:
                LD(_d, _l);
                return 4;
            case 0x6B:
                LD(_e, _l);
                return 4;
            case 0x6C:
                LD(_h, _l);
                return 4;
            case 0x6D:
                LD(_l, _l);
                return 4;
            case 0x6E:
                SetMemoryBusToRegValue(_hl);
                LD(_memory, _l);
                return 8;
            case 0x6F:
                LD(_a, _l);
                return 4;
            case 0x70:
                SetMemoryBusToRegValue(_hl);
                LD(_b, _hl);
                return 8;
            case 0x71:
                SetMemoryBusToRegValue(_hl);
                LD(_c, _hl);
                return 8;
            case 0x72:
                SetMemoryBusToRegValue(_hl);
                LD(_d, _hl);
                return 8;
            case 0x73:
                SetMemoryBusToRegValue(_hl);
                LD(_e, _hl);
                return 8;
            case 0x74:
                SetMemoryBusToRegValue(_hl);
                LD(_h, _hl);
                return 8;
            case 0x75:
                SetMemoryBusToRegValue(_hl);
                LD(_l, _hl);
                return 8;
            case 0x76:
                throw new ArgumentException("0x56 HALT called");
            case 0x77:
                SetMemoryBusToRegValue(_hl);
                LD(_a, _hl);
                return 8;
            case 0x78:
                LD(_b, _a);
                return 4;
            case 0x79:
                LD(_c, _a);
                return 4;
            case 0x7A:
                LD(_d, _a);
                return 4;
            case 0x7B:
                LD(_e, _a);
                return 4;
            case 0x7C:
                LD(_h, _a);
                return 4;
            case 0x7D:
                LD(_l, _a);
                return 4;
            case 0x7E:
                SetMemoryBusToRegValue(_hl);
                LD(_memory, _a);
                return 8;
            case 0x7F:
                LD(_a, _a);
                return 4;
#endregion
#region ALU8
            case 0x80:
                ADD8(_b);
                return 4;
            case 0x81:
                ADD8(_c);
                return 4;
            case 0x82:
                ADD8(_d);
                return 4;
            case 0x83:
                ADD8(_e);
                return 4;
            case 0x84:
                ADD8(_h);
                return 4;
            case 0x85:
                ADD8(_l);
                return 4;
            case 0x86:
                SetMemoryBusToRegValue(_hl);
                ADD8(_memory);
                return 8;
            case 0x87:
                ADD8(_a);
                return 4;
#endregion
            default:
                throw new ArgumentException($"Invalid opcode {opcode:X2}");
            }
        }
    }
}