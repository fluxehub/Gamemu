using Gamemu.Emulator.Processor.Addressing;

namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0xC5, Cycles = 16, Source = AddressingMode.RegisterBC)]
    [Instruction(Opcode = 0xD5, Cycles = 16, Source = AddressingMode.RegisterDE)]
    [Instruction(Opcode = 0xE5, Cycles = 16, Source = AddressingMode.RegisterHL)]
    [Instruction(Opcode = 0xF5, Cycles = 16, Source = AddressingMode.RegisterAF)]
    public class PUSH : ReadInstruction
    {
        private readonly MemoryMap _memory;
        private readonly Register16 _sp;
        
        public PUSH(ISource source, [SP] Register16 sp, MemoryMap memory, int cycles) : base(source, cycles)
        {
            _memory = memory;
            _sp = sp;
        }

        public override void Execute()
        {
            InstructionUtilities.Push(_sp, _memory, Source.Read());
        }
    }
}