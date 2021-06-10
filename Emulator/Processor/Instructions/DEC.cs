using Gamemu.Emulator.Processor.Addressing;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0x0B, Cycles = 8, Addressable = RegisterBC)]
    [Instruction(Opcode = 0x1B, Cycles = 8, Addressable = RegisterDE)]
    [Instruction(Opcode = 0x2B, Cycles = 8, Addressable = RegisterHL)]
    [Instruction(Opcode = 0x3B, Cycles = 8, Addressable = RegisterSP)]
    public class DEC16 : SameReadWriteInstruction
    {
        public DEC16(IAddressable addressable, int cycles) : base(addressable, cycles)
        {
        }

        public override void Execute()
        {
            // Cast to register type to use Decrement
            ((Register16) Addressable).Decrement();
        }
    }
    
    [Instruction(Opcode = 0x05, Addressable = RegisterB)]
    [Instruction(Opcode = 0x15, Addressable = RegisterD)]
    [Instruction(Opcode = 0x25, Addressable = RegisterH)]
    [Instruction(Opcode = 0x35, Addressable = AbsoluteHL)]
    [Instruction(Opcode = 0x0D, Addressable = RegisterC)]
    [Instruction(Opcode = 0x1D, Addressable = RegisterE)]
    [Instruction(Opcode = 0x2D, Addressable = RegisterL)]
    [Instruction(Opcode = 0x3D, Addressable = RegisterA)]
    public class DEC : SameReadWriteInstruction
    {
        private readonly FlagsRegister _flags;
        
        public DEC(IAddressable addressable, FlagsRegister flags, int cycles) : base(addressable, cycles)
        {
            _flags = flags;
        }

        public override void Execute()
        {
            var value = Addressable.Read();
            var incremented = value + 1;
            Addressable.Write(value);
            InstructionUtilities.SetFlags(_flags, value, -1, true, false, false);
        }
    }
}