using Gamemu.Emulator.Processor.Addressing;

namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0xC7, Cycles = 16, RestartAddress = 0x00)]
    [Instruction(Opcode = 0xCF, Cycles = 16, RestartAddress = 0x08)]
    [Instruction(Opcode = 0xD7, Cycles = 16, RestartAddress = 0x10)]
    [Instruction(Opcode = 0xDF, Cycles = 16, RestartAddress = 0x18)]
    [Instruction(Opcode = 0xE7, Cycles = 16, RestartAddress = 0x20)]
    [Instruction(Opcode = 0xEF, Cycles = 16, RestartAddress = 0x28)]
    [Instruction(Opcode = 0xF7, Cycles = 16, RestartAddress = 0x30)]
    [Instruction(Opcode = 0xFF, Cycles = 16, RestartAddress = 0x38)]
    public class RST : Instruction
    {
        private readonly int _address;
        private readonly MemoryMap _memory;
        private readonly Register16 _sp;
        private readonly Register16 _pc;
        
        public RST([SP] Register16 sp, [PC] Register16 pc, MemoryMap memoryMap, [RestartAddress] int address,
            int cycles) : base(cycles)
        {
            _address = address;
            _memory = memoryMap;
            _sp = sp;
            _pc = pc;
        }

        public override void Execute()
        {
            // Push PC value onto stack
            var pcAddress = _pc.Read();
            var stackAddress = _sp.Read() - 1;

            _memory[stackAddress] = pcAddress >> 8;
            _memory[stackAddress - 1] =  pcAddress & 0xFF;
            
            // Set PC to reset address
            _pc.Write(_address);
            
            // Set SP to top of stack
            _sp.Write(stackAddress - 1);
        }
    }
}