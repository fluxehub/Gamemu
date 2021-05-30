using System;

namespace Gamemu.Emulator.CPU
{
    public partial class CPU
    {
        private void LD(ISource source, IDest dest)
        {
            dest.Write(source.Read());
        }

        private void ADD8(ISource source)
        {
            var result = _a.Read() + source.Read();
            _a.Write(result);
            _f.ZeroFlag = result == 0;
            _f.SubtractionFlag = true;

        }

        private void INC16(CPUCombinedRegister register)
        {
            register.Write(register.Read() + 1);
        }

        private void DEC16(CPUCombinedRegister register)
        {
            register.Write(register.Read() - 1);
        }

        private void SCF()
        {
            _f.SubtractionFlag = false;
            _f.HalfCarryFlag = false;
            _f.CarryFlag = true;
        }
    }
}