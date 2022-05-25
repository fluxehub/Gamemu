using Gamemu.Emulator.Processor.Addressing.Modes;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0x0B, Cycles = 8, Addressable = RegisterBc)]
[Instruction(Opcode = 0x1B, Cycles = 8, Addressable = RegisterDe)]
[Instruction(Opcode = 0x2B, Cycles = 8, Addressable = RegisterHl)]
[Instruction(Opcode = 0x3B, Cycles = 8, Addressable = RegisterSp)]
public class Dec16 : UpdateInstruction
{
    public Dec16(IAddressable addressable, int cycles) : base(addressable, cycles)
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
[Instruction(Opcode = 0x35, Addressable = AbsoluteHl)]
[Instruction(Opcode = 0x0D, Addressable = RegisterC)]
[Instruction(Opcode = 0x1D, Addressable = RegisterE)]
[Instruction(Opcode = 0x2D, Addressable = RegisterL)]
[Instruction(Opcode = 0x3D, Addressable = RegisterA)]
public class Dec : UpdateInstruction
{
    private readonly FlagsRegister _flags;
        
    public Dec(IAddressable addressable, FlagsRegister flags, int cycles) : base(addressable, cycles)
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