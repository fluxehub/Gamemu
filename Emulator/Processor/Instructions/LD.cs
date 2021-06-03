using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions
{
    // LOL
    [Instruction(Opcode = 0x01, Cycles = 12, Source = Immediate16, Dest = RegisterBC)]
    [Instruction(Opcode = 0x11, Cycles = 12, Source = Immediate16, Dest = RegisterDE)]
    [Instruction(Opcode = 0x21, Cycles = 12, Source = Immediate16, Dest = RegisterHL)]
    [Instruction(Opcode = 0x31, Cycles = 12, Source = Immediate16, Dest = RegisterSP)]
    
    [Instruction(Opcode = 0x02, Cycles = 8, Source = RegisterA, Dest = AbsoluteBC)]
    [Instruction(Opcode = 0x12, Cycles = 8, Source = RegisterA, Dest = AbsoluteDE)]
    [Instruction(Opcode = 0x22, Cycles = 8, Source = RegisterA, Dest = AbsoluteHLInc)]
    [Instruction(Opcode = 0x32, Cycles = 8, Source = RegisterA, Dest = AbsoluteHLDec)]
    
    [Instruction(Opcode = 0x06, Cycles = 8, Source = Immediate, Dest = RegisterB)]
    [Instruction(Opcode = 0x16, Cycles = 8, Source = Immediate, Dest = RegisterD)]
    [Instruction(Opcode = 0x26, Cycles = 8, Source = Immediate, Dest = RegisterH)]
    [Instruction(Opcode = 0x36, Cycles = 12, Source = Immediate, Dest = AbsoluteHL)]

    [Instruction(Opcode = 0x0A, Cycles = 8, Source = AbsoluteBC, Dest = RegisterA)]
    [Instruction(Opcode = 0x1A, Cycles = 8, Source = AbsoluteDE, Dest = RegisterA)]
    [Instruction(Opcode = 0x2A, Cycles = 8, Source = AbsoluteHLInc, Dest = RegisterA)]
    [Instruction(Opcode = 0x3A, Cycles = 8, Source = AbsoluteHLDec, Dest = RegisterA)]
    
    [Instruction(Opcode = 0x0E, Cycles = 8, Source = Immediate, Dest = RegisterC)]
    [Instruction(Opcode = 0x1E, Cycles = 8, Source = Immediate, Dest = RegisterE)]
    [Instruction(Opcode = 0x2E, Cycles = 8, Source = Immediate, Dest = RegisterL)]
    [Instruction(Opcode = 0x3E, Cycles = 8, Source = Immediate, Dest = RegisterA)]
    
    [Instruction(Opcode = 0x40, Source = RegisterB, Dest = RegisterB)]
    [Instruction(Opcode = 0x41, Source = RegisterC, Dest = RegisterB)]
    [Instruction(Opcode = 0x42, Source = RegisterD, Dest = RegisterB)]
    [Instruction(Opcode = 0x43, Source = RegisterE, Dest = RegisterB)]
    [Instruction(Opcode = 0x44, Source = RegisterH, Dest = RegisterB)]
    [Instruction(Opcode = 0x45, Source = RegisterL, Dest = RegisterB)]
    [Instruction(Opcode = 0x46, Cycles = 8, Source = AbsoluteHL, Dest = RegisterB)]
    [Instruction(Opcode = 0x47, Source = RegisterA, Dest = RegisterB)]
    
    [Instruction(Opcode = 0x48, Source = RegisterB, Dest = RegisterC)]
    [Instruction(Opcode = 0x49, Source = RegisterC, Dest = RegisterC)]
    [Instruction(Opcode = 0x4A, Source = RegisterD, Dest = RegisterC)]
    [Instruction(Opcode = 0x4B, Source = RegisterE, Dest = RegisterC)]
    [Instruction(Opcode = 0x4C, Source = RegisterH, Dest = RegisterC)]
    [Instruction(Opcode = 0x4D, Source = RegisterL, Dest = RegisterC)]
    [Instruction(Opcode = 0x4E, Cycles = 8, Source = AbsoluteHL, Dest = RegisterC)]
    [Instruction(Opcode = 0x4F, Source = RegisterA, Dest = RegisterC)]
    
    [Instruction(Opcode = 0x50, Source = RegisterB, Dest = RegisterD)]
    [Instruction(Opcode = 0x51, Source = RegisterC, Dest = RegisterD)]
    [Instruction(Opcode = 0x52, Source = RegisterD, Dest = RegisterD)]
    [Instruction(Opcode = 0x53, Source = RegisterE, Dest = RegisterD)]
    [Instruction(Opcode = 0x54, Source = RegisterH, Dest = RegisterD)]
    [Instruction(Opcode = 0x55, Source = RegisterL, Dest = RegisterD)]
    [Instruction(Opcode = 0x56, Cycles = 8, Source = AbsoluteHL, Dest = RegisterD)]
    [Instruction(Opcode = 0x57, Source = RegisterA, Dest = RegisterD)]
    
    [Instruction(Opcode = 0x58, Source = RegisterB, Dest = RegisterE)]
    [Instruction(Opcode = 0x59, Source = RegisterC, Dest = RegisterE)]
    [Instruction(Opcode = 0x5A, Source = RegisterD, Dest = RegisterE)]
    [Instruction(Opcode = 0x5B, Source = RegisterE, Dest = RegisterE)]
    [Instruction(Opcode = 0x5C, Source = RegisterH, Dest = RegisterE)]
    [Instruction(Opcode = 0x5D, Source = RegisterL, Dest = RegisterE)]
    [Instruction(Opcode = 0x5E, Cycles = 8, Source = AbsoluteHL, Dest = RegisterE)]
    [Instruction(Opcode = 0x5F, Source = RegisterA, Dest = RegisterE)]
    
    [Instruction(Opcode = 0x60, Source = RegisterB, Dest = RegisterH)]
    [Instruction(Opcode = 0x61, Source = RegisterC, Dest = RegisterH)]
    [Instruction(Opcode = 0x62, Source = RegisterD, Dest = RegisterH)]
    [Instruction(Opcode = 0x63, Source = RegisterE, Dest = RegisterH)]
    [Instruction(Opcode = 0x64, Source = RegisterH, Dest = RegisterH)]
    [Instruction(Opcode = 0x65, Source = RegisterL, Dest = RegisterH)]
    [Instruction(Opcode = 0x66, Cycles = 8, Source = AbsoluteHL, Dest = RegisterH)]
    [Instruction(Opcode = 0x67, Source = RegisterA, Dest = RegisterH)]
    
    [Instruction(Opcode = 0x68, Source = RegisterB, Dest = RegisterL)]
    [Instruction(Opcode = 0x69, Source = RegisterC, Dest = RegisterL)]
    [Instruction(Opcode = 0x6A, Source = RegisterD, Dest = RegisterL)]
    [Instruction(Opcode = 0x6B, Source = RegisterE, Dest = RegisterL)]
    [Instruction(Opcode = 0x6C, Source = RegisterH, Dest = RegisterL)]
    [Instruction(Opcode = 0x6D, Source = RegisterL, Dest = RegisterL)]
    [Instruction(Opcode = 0x6E, Cycles = 8, Source = AbsoluteHL, Dest = RegisterL)]
    [Instruction(Opcode = 0x6F, Source = RegisterA, Dest = RegisterL)]
    
    [Instruction(Opcode = 0x70, Cycles = 8, Source = RegisterB, Dest = AbsoluteHL)]
    [Instruction(Opcode = 0x71, Cycles = 8, Source = RegisterC, Dest = AbsoluteHL)]
    [Instruction(Opcode = 0x72, Cycles = 8, Source = RegisterD, Dest = AbsoluteHL)]
    [Instruction(Opcode = 0x73, Cycles = 8, Source = RegisterE, Dest = AbsoluteHL)]
    [Instruction(Opcode = 0x74, Cycles = 8, Source = RegisterH, Dest = AbsoluteHL)]
    [Instruction(Opcode = 0x75, Cycles = 8, Source = RegisterL, Dest = AbsoluteHL)]
    [Instruction(Opcode = 0x77, Cycles = 8, Source = RegisterA, Dest = AbsoluteHL)]
    
    [Instruction(Opcode = 0x78, Source = RegisterB, Dest = RegisterA)]
    [Instruction(Opcode = 0x79, Source = RegisterC, Dest = RegisterA)]
    [Instruction(Opcode = 0x7A, Source = RegisterD, Dest = RegisterA)]
    [Instruction(Opcode = 0x7B, Source = RegisterE, Dest = RegisterA)]
    [Instruction(Opcode = 0x7C, Source = RegisterH, Dest = RegisterA)]
    [Instruction(Opcode = 0x7D, Source = RegisterL, Dest = RegisterA)]
    [Instruction(Opcode = 0x7E, Cycles = 8, Source = AbsoluteHL, Dest = RegisterA)]
    [Instruction(Opcode = 0x7F, Source = RegisterA, Dest = RegisterA)]
    
    [Instruction(Opcode = 0xE0, Cycles = 12, Source = RegisterA, Dest = IOImmediate)]
    [Instruction(Opcode = 0xF0, Cycles = 12, Source = IOImmediate, Dest = RegisterA)]
    
    [Instruction(Opcode = 0xE2, Cycles = 12, Source = RegisterA, Dest = IORegisterC)]
    [Instruction(Opcode = 0xF2, Cycles = 12, Source = IORegisterC, Dest = RegisterA)]
    
    [Instruction(Opcode = 0xF9, Cycles = 8, Source = RegisterHL, Dest = RegisterSP)]
    
    [Instruction(Opcode = 0xEA, Cycles = 16, Source = RegisterA, Dest = AbsoluteImmediate)]
    [Instruction(Opcode = 0xFA, Cycles = 16, Source = AbsoluteImmediate, Dest = RegisterA)]
    public class LD : ReadWriteInstruction
    {
        public LD(ISource source, IDest dest, int cycles) : base(source, dest, cycles)
        {
        }

        public override void Execute()
        {
            Dest.Write(Source.Read());
        }
    }
}