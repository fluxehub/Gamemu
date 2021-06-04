using Gamemu.Emulator.Processor.Addressing;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0x03, Cycles = 8, Addressable = RegisterBC)]
    [Instruction(Opcode = 0x13, Cycles = 8, Addressable = RegisterDE)]
    [Instruction(Opcode = 0x23, Cycles = 8, Addressable = RegisterHL)]
    [Instruction(Opcode = 0x33, Cycles = 8, Addressable = RegisterSP)]
    public class INC16 : SameReadWriteInstruction
    {
        public INC16(IAddressable addressable, int cycles) : base(addressable, cycles)
        {
        }

        public override void Execute()
        {
            // Cast to register type to use Increment
            ((Register16) Addressable).Increment();
        }
    }
    
    [Instruction(Opcode = 0x04, Addressable = RegisterB)]
    [Instruction(Opcode = 0x14, Addressable = RegisterD)]
    [Instruction(Opcode = 0x24, Addressable = RegisterH)]
    [Instruction(Opcode = 0x34, Addressable = AbsoluteHL)]
    [Instruction(Opcode = 0x0C, Addressable = RegisterC)]
    [Instruction(Opcode = 0x1C, Addressable = RegisterE)]
    [Instruction(Opcode = 0x2C, Addressable = RegisterL)]
    [Instruction(Opcode = 0x3C, Addressable = RegisterA)]
    public class INC : SameReadWriteInstruction
    {
        private readonly FlagsRegister _flags;
        
        public INC(IAddressable addressable, FlagsRegister flags, int cycles) : base(addressable, cycles)
        {
            _flags = flags;
        }

        public override void Execute()
        {
            var value = Addressable.Read();
            var incremented = value + 1;
            Addressable.Write(value);
            // TODO: Set flags
        }
    }
}