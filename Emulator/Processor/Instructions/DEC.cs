using Gamemu.Emulator.Processor.Addressing;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0x0B, Cycles = 8, Addressable = RegisterBC)]
    [Instruction(Opcode = 0x1B, Cycles = 8, Addressable = RegisterDE)]
    [Instruction(Opcode = 0x2B, Cycles = 8, Addressable = RegisterHL)]
    [Instruction(Opcode = 0x3B, Cycles = 8, Addressable = RegisterSP)]
    public class DEC16 : SameRegisterInstruction
    {
        public DEC16(Register register, int cycles) : base(register, cycles)
        {
        }

        public override void Execute()
        {
            Register.Decrement();
        }
    }
}