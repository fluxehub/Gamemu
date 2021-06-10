using Gamemu.Emulator.Processor.Addressing;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0x80, Source = RegisterB)]
    [Instruction(Opcode = 0x81, Source = RegisterC)]
    [Instruction(Opcode = 0x82, Source = RegisterD)]
    [Instruction(Opcode = 0x83, Source = RegisterE)]
    [Instruction(Opcode = 0x84, Source = RegisterH)]
    [Instruction(Opcode = 0x85, Source = RegisterL)]
    [Instruction(Opcode = 0x86, Cycles = 8, Source = AbsoluteHL)]
    [Instruction(Opcode = 0x87, Source = RegisterA)]
    
    [Instruction(Opcode = 0xC6, Source = AddressingMode.Immediate)]
    public class ADD : ReadInstruction
    {
        private readonly Register _a;
        private readonly FlagsRegister _flags;

        public ADD(ISource source, [A] Register a, FlagsRegister flags, int cycles) : base(source, cycles)
        {
            _a = a;
            _flags = flags;
        }

        public override void Execute()
        {
            var a = _a.Read();
            var b = Source.Read();
            
            _a.Write(a + b);
            InstructionUtilities.SetFlags(_flags, a, b, false);
        }
    }
    
    [Instruction(Opcode = 0x09, Cycles = 8, Source = RegisterBC, Dest = RegisterHL)]
    [Instruction(Opcode = 0x19, Cycles = 8, Source = RegisterDE, Dest = RegisterHL)]
    [Instruction(Opcode = 0x29, Cycles = 8, Source = RegisterHL, Dest = RegisterHL)]
    [Instruction(Opcode = 0x39, Cycles = 8, Source = RegisterSP, Dest = RegisterHL)]
    public class ADDHL : ReadWriteInstruction
    {
        private readonly FlagsRegister _flags;

        public ADDHL(ISource source, IDest dest, FlagsRegister flags, int cycles) : base(source, dest, cycles)
        {
            _flags = flags;
        }

        public override void Execute()
        {
            // Guaranteed to be a Register16
            var a = ((Register16) Dest).Read();
            var b = Source.Read();
            
            Dest.Write(a + b);
            InstructionUtilities.SetFlags(_flags, a, b, false, true, false);
        }
    }
    
    [Instruction(Opcode = 0xE8, Cycles = 16, Source = AddressingMode.ImmediateSigned)]
    public class ADDSP : ReadInstruction
    {
        private readonly Register16 _sp;
        private readonly FlagsRegister _flags;

        public ADDSP(ISource source, [SP] Register16 sp, FlagsRegister flags, int cycles) : base(source, cycles)
        {
            _sp = sp;
            _flags = flags;
        }

        public override void Execute()
        {
            var a = _sp.Read();
            var b = Source.Read();
            
            _sp.Write(a + b);
            // Don't use double width as the second operand is 8 bit
            InstructionUtilities.SetFlags(_flags, a, b, false);
            
            // On the off chance the stack pointer wraps around to zero, the zero flag still stays false
            _flags.ZeroFlag = false;
        }
    }
}