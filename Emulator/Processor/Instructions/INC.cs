using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions
{
    // Yes it's dumb that the source and the destination are the same
    // but I don't care
    [Instruction(Opcode = 0x03, Cycles = 8, Source = RegisterBC, Dest = RegisterBC)]
    [Instruction(Opcode = 0x13, Cycles = 8, Source = RegisterDE, Dest = RegisterDE)]
    [Instruction(Opcode = 0x23, Cycles = 8, Source = RegisterHL, Dest = RegisterHL)]
    [Instruction(Opcode = 0x33, Cycles = 8, Source = RegisterSP, Dest = RegisterSP)]
    public class INC16 : ReadWriteInstruction
    {
        public INC16(CPU cpu, int cycles, ISource source, IDest dest) : base(cpu, cycles, source, dest)
        {
        }

        public override void Execute()
        {
            Dest.Write(Source.Read() + 1);
        }
    }
    
    [Instruction(Opcode = 0x0B, Cycles = 8, Source = RegisterBC, Dest = RegisterBC)]
    [Instruction(Opcode = 0x1B, Cycles = 8, Source = RegisterDE, Dest = RegisterDE)]
    [Instruction(Opcode = 0x2B, Cycles = 8, Source = RegisterHL, Dest = RegisterHL)]
    [Instruction(Opcode = 0x3B, Cycles = 8, Source = RegisterSP, Dest = RegisterSP)]
    public class DEC16 : ReadWriteInstruction
    {
        public DEC16(CPU cpu, int cycles, ISource source, IDest dest) : base(cpu, cycles, source, dest)
        {
        }

        public override void Execute()
        {
            Dest.Write(Source.Read() - 1);
        }
    }
}