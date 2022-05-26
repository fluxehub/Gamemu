using Gamemu.Emulator.Processor.Addressing;
using Gamemu.Emulator.Processor.Addressing.Modes;
using static Gamemu.Emulator.Processor.Instructions.ConditionType;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0xC9, Cycles = 16)]
public class Ret : Instruction    
{
    private readonly Register16 _sp;
    private readonly Register16 _pc;
    private readonly MemoryMap _memory;
        
    public Ret([Sp] Register16 sp, [Pc] Register16 pc, MemoryMap memory, int cycles) : base(cycles)
    {
        _sp = sp;
        _pc = pc;
        _memory = memory;
    }

    public override void Execute()
    {
        var address = InstructionUtilities.Pop(_sp, _memory);
        InstructionUtilities.Jump(_pc, address);
    }
}
    
[Instruction(Opcode = 0xC0, Cycles = 8, CyclesAlternate = 20, JumpCondition = NotZero)]
[Instruction(Opcode = 0xC8, Cycles = 8, CyclesAlternate = 20, JumpCondition = Zero)]
[Instruction(Opcode = 0xD0, Cycles = 8, CyclesAlternate = 20, JumpCondition = NotCarry)]
[Instruction(Opcode = 0xD8, Cycles = 8, CyclesAlternate = 20, JumpCondition = Carry)]
public class RetConditional : Instruction
{
    private readonly Register16 _sp;
    private readonly Register16 _pc;
    private readonly MemoryMap _memory;
    private readonly Condition _shouldJump;
    private readonly int _cyclesNoBranch;
    private readonly int _cyclesBranch;
        
    public RetConditional([Sp] Register16 sp, [Pc] Register16 pc, MemoryMap memory, Condition shouldJump, int cyclesNoBranch, [Alternate] int cyclesBranch) : base(cyclesNoBranch)
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
            var address = InstructionUtilities.Pop(_sp, _memory);
            InstructionUtilities.Jump(_pc, address);
        }
        else
        {
            Cycles = _cyclesNoBranch;
        }
    }
}
    
[Instruction(Opcode = 0xD9, Cycles = 16)]
public class RetI : Instruction
{
    private readonly Cpu _cpu;
    private readonly Register16 _sp;
    private readonly Register16 _pc;
    private readonly MemoryMap _memory;
        
    public RetI(Cpu cpu, [Sp] Register16 sp, [Pc] Register16 pc, MemoryMap memory, int cycles) : base(cycles)
    {
        _cpu = cpu;
        _sp = sp;
        _pc = pc;
        _memory = memory;
    }

    public override void Execute()
    {
        var address = InstructionUtilities.Pop(_sp, _memory);
        InstructionUtilities.Jump(_pc, address);
        _cpu.InterruptStatus = InterruptStatus.Enabled;
    }
}