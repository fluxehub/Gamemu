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
            var address = _sp.Read();
            
            // Get the low and high bytes from the stack
            var low = _memory[address];
            var high = _memory[address + 1];
            
            // Write the 16-bit value to the registers
            Dest.Write((high << 8) | low);
            
            // Set SP to top of stack
            _sp.Write(address + 2);
        }
    }
}