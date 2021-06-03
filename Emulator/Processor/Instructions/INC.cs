using Gamemu.Emulator.Processor.Addressing;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions
{
    // Yes it's still dumb that the source and the destination are the same
    // but I don't care
    [Instruction(Opcode = 0x03, Cycles = 8, Addressable = RegisterBC)]
    [Instruction(Opcode = 0x13, Cycles = 8, Addressable = RegisterDE)]
    [Instruction(Opcode = 0x23, Cycles = 8, Addressable = RegisterHL)]
    [Instruction(Opcode = 0x33, Cycles = 8, Addressable = RegisterSP)]
    public class INC16 : SameRegisterInstruction
    {
        public INC16(Register register, int cycles) : base(register, cycles)
        {
        }

        public override void Execute()
        {
            Register.Increment();
        }
    }
}