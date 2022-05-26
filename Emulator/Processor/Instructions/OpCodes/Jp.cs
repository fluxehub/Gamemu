using Gamemu.Emulator.Processor.Addressing.Modes;
using static Gamemu.Emulator.Processor.Instructions.ConditionType;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0xC3, Cycles = 16, Source = ImmediateValue16)]
[Instruction(Opcode = 0xE9, Source = RegisterHl)]
public class Jp : ReadInstruction
{
    private readonly Register16 _pc;
        
    public Jp(ISource source, [Pc] Register16 pc, int cycles) : base(source, cycles)
    {
        _pc = pc;
    }
        
    public override void Execute()
    {
        InstructionUtilities.Jump(_pc, Source.Read());
    }
}

[Instruction(Opcode = 0xC2, Cycles = 12, CyclesAlternate = 16, Source = ImmediateValue16, JumpCondition = NotZero)]
[Instruction(Opcode = 0xCA, Cycles = 12, CyclesAlternate = 16, Source = ImmediateValue16, JumpCondition = Zero)]
[Instruction(Opcode = 0xD2, Cycles = 12, CyclesAlternate = 16, Source = ImmediateValue16, JumpCondition = NotCarry)]
[Instruction(Opcode = 0xDA, Cycles = 12, CyclesAlternate = 16, Source = ImmediateValue16, JumpCondition = Carry)]
public class JpConditional : ReadInstruction
{
    private readonly Register16 _pc;
    private readonly Condition _shouldJump;
    private readonly int _cyclesNoBranch;
    private readonly int _cyclesBranch;
        
    public JpConditional(ISource source, [Pc] Register16 pc, Condition shouldJump, int cyclesNoBranch, [Alternate] int cyclesBranch) : base(source, cyclesNoBranch)
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
            InstructionUtilities.Jump(_pc, Source.Read());
        }
        else
        {
            Cycles = _cyclesNoBranch;
        }
    }
}