using Gamemu.Emulator.Processor.Addressing.Modes;
using static Gamemu.Emulator.Processor.Instructions.ConditionType;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0x18, Cycles = 12, Source = ImmediateValue8Signed)]
public class Jr : ReadInstruction
{
    private readonly Register16 _pc;
        
    public Jr(ISource source, [Pc] Register16 pc, int cycles) : base(source, cycles)
    {
        _pc = pc;
    }
        
    public override void Execute()
    {
        InstructionUtilities.Jump(_pc, _pc.Read() + Source.Read());
    }  
}

[Instruction(Opcode = 0x20, Cycles = 8, CyclesAlternate = 12, Source = ImmediateValue8Signed, JumpCondition = NotZero)]
[Instruction(Opcode = 0x30, Cycles = 8, CyclesAlternate = 12, Source = ImmediateValue8Signed, JumpCondition = Zero)]
[Instruction(Opcode = 0x28, Cycles = 8, CyclesAlternate = 12, Source = ImmediateValue8Signed, JumpCondition = NotCarry)]
[Instruction(Opcode = 0x38, Cycles = 8, CyclesAlternate = 12, Source = ImmediateValue8Signed, JumpCondition = Carry)]
public class JrConditional : ReadInstruction
{
    private readonly Register16 _pc;
    private readonly Condition _shouldJump;
    private readonly int _cyclesNoBranch;
    private readonly int _cyclesBranch;
        
    public JrConditional(ISource source, [Pc] Register16 pc, Condition shouldJump, int cyclesNoBranch, [Alternate] int cyclesBranch) : base(source, cyclesNoBranch)
    {
        _pc = pc;
        _shouldJump = shouldJump;
        _cyclesNoBranch = cyclesNoBranch;
        _cyclesBranch = cyclesBranch;
    }
        
    public override void Execute()
    {
        if (_shouldJump)
        {
            Cycles = _cyclesBranch;
            InstructionUtilities.Jump(_pc, _pc.Read() + Source.Read());
        }
        else
        {
            Cycles = _cyclesNoBranch;
        }
    }
}