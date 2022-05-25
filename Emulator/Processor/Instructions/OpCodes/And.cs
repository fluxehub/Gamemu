using Gamemu.Emulator.Processor.Addressing.Modes;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0xA0, Source = RegisterB)]
[Instruction(Opcode = 0xA1, Source = RegisterC)]
[Instruction(Opcode = 0xA2, Source = RegisterD)]
[Instruction(Opcode = 0xA3, Source = RegisterE)]
[Instruction(Opcode = 0xA4, Source = RegisterH)]
[Instruction(Opcode = 0xA5, Source = RegisterL)]
[Instruction(Opcode = 0xA6, Cycles = 8, Source = AbsoluteHl)]
[Instruction(Opcode = 0xA7, Source = RegisterA)]
[Instruction(Opcode = 0xE6, Cycles = 8, Source = ImmediateValue8)]
public class And : ReadInstruction
{
    private readonly Register _a;
    private readonly FlagsRegister _flags;
        
    public And([A] Register a, ISource source, FlagsRegister flags, int cycles) : base(source, cycles)
    {
        _a = a;
        _flags = flags;
    }

    public override void Execute()
    {
        var result = _a.Read() & Source.Read();
            
        _flags.ZeroFlag = result == 0;
        _flags.SubtractionFlag = false;
        _flags.HalfCarryFlag = true;
        _flags.CarryFlag = false;
            
        _a.Write(result);
    }
}