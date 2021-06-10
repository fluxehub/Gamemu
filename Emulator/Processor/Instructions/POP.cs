using Gamemu.Emulator.Processor.Addressing;

namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0xC1, Cycles = 12, Dest = AddressingMode.RegisterBC)]
    [Instruction(Opcode = 0xD1, Cycles = 12, Dest = AddressingMode.RegisterDE)]
    [Instruction(Opcode = 0xE1, Cycles = 12, Dest = AddressingMode.RegisterHL)]
    [Instruction(Opcode = 0xF1, Cycles = 12, Dest = AddressingMode.RegisterAF)]
    public class POP : WriteInstruction
    {
        private readonly MemoryMap _memory;
        private readonly Register16 _sp;
        
        public POP(IDest dest, [SP] Register16 sp, MemoryMap memory, int cycles) : base(dest, cycles)
        {
            _memory = memory;
            _sp = sp;
        }

        public override void Execute()
        {
            // Write the stack value to the registers
            Dest.Write(InstructionUtilities.Pop(_sp, _memory));
        }
    }
}