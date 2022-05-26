using Gamemu.Emulator.Processor.Addressing.Modes;
using static Gamemu.Emulator.Processor.Instructions.ConditionType;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0xCD, Cycles = 24, Source = ImmediateValue16)]
public class Call : ReadInstruction
{
    private readonly Register16 _sp;
    private readonly Register16 _pc;
    private readonly MemoryMap _memory;
    
    public Call(ISource source, [Sp] Register16 sp, [Pc] Register16 pc, MemoryMap memory, int cycles) : base(source, cycles)
    {
        _sp = sp;
        _pc = pc;
        _memory = memory;
    }

    public override void Execute()
    {
        InstructionUtilities.Push(_sp, _memory, _pc.Read());
        InstructionUtilities.Jump(_pc, Source.Read());
    }
}


[Instruction(Opcode = 0xC4, Cycles = 12, CyclesAlternate = 24, Source = ImmediateValue16, JumpCondition = NotZero)]
[Instruction(Opcode = 0xCC, Cycles = 12, CyclesAlternate = 24, Source = ImmediateValue16, JumpCondition = Zero)]
[Instruction(Opcode = 0xD4, Cycles = 12, CyclesAlternate = 24, Source = ImmediateValue16, JumpCondition = NotCarry)]
[Instruction(Opcode = 0xDC, Cycles = 12, CyclesAlternate = 24, Source = ImmediateValue16, JumpCondition = Carry)]
public class CallConditional : ReadInstruction
{
    private readonly Register16 _sp;
    private readonly Register16 _pc;
    private readonly MemoryMap _memory;
    private readonly Condition _shouldJump;
    private readonly int _cyclesNoBranch;
    private readonly int _cyclesBranch;
        
    public CallConditional(ISource source, [Sp] Register16 sp, [Pc] Register16 pc, MemoryMap memory, Condition shouldJump, int cyclesNoBranch, [Alternate] int cyclesBranch) : base(source, cyclesNoBranch)
    {
        _sp = sp;
        _pc = pc;
        _memory = memory;
        _shouldJump = shouldJump;
        _cyclesNoBranch = cyclesNoBranch;
        _cyclesBranch = cyclesBranch;
    }

    public override void Execute()
    {
        if (_shouldJump)
        {
            Cycles = _cyclesBranch;
            InstructionUtilities.Push(_sp, _memory, _pc.Read());
            InstructionUtilities.Jump(_pc, Source.Read());
        } else {
            Cycles = _cyclesNoBranch;
        }
    }
}