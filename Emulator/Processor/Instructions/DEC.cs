using Gamemu.Emulator.Processor.Addressing;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions
{
    // Yes it's dumb that the source and the destination are the same
    // but I don't care
    [Instruction(Opcode = 0x0B, Cycles = 8, Source = RegisterBC, Dest = RegisterBC)]
    [Instruction(Opcode = 0x1B, Cycles = 8, Source = RegisterDE, Dest = RegisterDE)]
    [Instruction(Opcode = 0x2B, Cycles = 8, Source = RegisterHL, Dest = RegisterHL)]
    [Instruction(Opcode = 0x3B, Cycles = 8, Source = RegisterSP, Dest = RegisterSP)]
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