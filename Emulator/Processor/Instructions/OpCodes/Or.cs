using Gamemu.Emulator.Processor.Addressing;
using Gamemu.Emulator.Processor.Addressing.Modes;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0xB0, Source = RegisterB)]
[Instruction(Opcode = 0xB1, Source = RegisterC)]
[Instruction(Opcode = 0xB2, Source = RegisterD)]
[Instruction(Opcode = 0xB3, Source = RegisterE)]
[Instruction(Opcode = 0xB4, Source = RegisterH)]
[Instruction(Opcode = 0xB5, Source = RegisterL)]
[Instruction(Opcode = 0xB6, Cycles = 8, Source = AbsoluteHl)]
[Instruction(Opcode = 0xB7, Source = RegisterA)]
[Instruction(Opcode = 0xF6, Cycles = 8, Source = AddressingMode.ImmediateValue8)]
public class Or : ReadInstruction
{
    private readonly Register _a;
    private readonly FlagsRegister _flags;
        
    public Or([A] Register a, ISource source, FlagsRegister flags, int cycles) : base(source, cycles)
    {
        _a = a;
        _flags = flags;
    }

    public override void Execute()
    {
        var result = _a.Read() | Source.Read();
            
        _flags.ZeroFlag = result == 0;
        _flags.SubtractionFlag = false;
        _flags.HalfCarryFlag = false;
        _flags.CarryFlag = false;
            
        _a.Write(result);
    }
}