using Gamemu.Emulator.Processor.Addressing;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0x98, Source = RegisterB)]
    [Instruction(Opcode = 0x99, Source = RegisterC)]
    [Instruction(Opcode = 0x9A, Source = RegisterD)]
    [Instruction(Opcode = 0x9B, Source = RegisterE)]
    [Instruction(Opcode = 0x9C, Source = RegisterH)]
    [Instruction(Opcode = 0x9D, Source = RegisterL)]
    [Instruction(Opcode = 0x9E, Cycles = 8, Source = AbsoluteHL)]
    [Instruction(Opcode = 0x9F, Source = RegisterA)]
    
    [Instruction(Opcode = 0xDE, Cycles = 8, Source = AddressingMode.Immediate)]
    public class SBC : ReadInstruction
    {
        private readonly Register _a;
        private readonly FlagsRegister _flags;

        public SBC(ISource source, [A] Register a, FlagsRegister flags, int cycles) : base(source, cycles)
        {
            _a = a;
            _flags = flags;
        }

        public override void Execute()
        {
            var a = _a.Read();
            var b = -Source.Read();
            var carry = _flags.CarryFlag ? -1 : 0;
            
            _a.Write(a + b + carry);
            InstructionUtilities.SetFlags(_flags, a, b + carry, true, false, true, true, true);
        }
    }
}