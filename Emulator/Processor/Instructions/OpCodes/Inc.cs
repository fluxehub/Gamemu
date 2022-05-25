using Gamemu.Emulator.Processor.Addressing.Modes;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0x03, Cycles = 8, Addressable = RegisterBc)]
[Instruction(Opcode = 0x13, Cycles = 8, Addressable = RegisterDe)]
[Instruction(Opcode = 0x23, Cycles = 8, Addressable = RegisterHl)]
[Instruction(Opcode = 0x33, Cycles = 8, Addressable = RegisterSp)]
public class Inc16 : UpdateInstruction
{
    public Inc16(IAddressable addressable, int cycles) : base(addressable, cycles)
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
[Instruction(Opcode = 0x34, Addressable = AbsoluteHl)]
[Instruction(Opcode = 0x0C, Addressable = RegisterC)]
[Instruction(Opcode = 0x1C, Addressable = RegisterE)]
[Instruction(Opcode = 0x2C, Addressable = RegisterL)]
[Instruction(Opcode = 0x3C, Addressable = RegisterA)]
public class Inc : UpdateInstruction
{
    private readonly FlagsRegister _flags;
        
    public Inc(IAddressable addressable, FlagsRegister flags, int cycles) : base(addressable, cycles)
    {
        _flags = flags;
    }

    public override void Execute()
    {
        var value = Addressable.Read();
        var incremented = value + 1;
        Addressable.Write(value);
        InstructionUtilities.SetFlags(_flags, value, 1, false, false, false);
    }
}