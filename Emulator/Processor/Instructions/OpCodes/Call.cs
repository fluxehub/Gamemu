using Gamemu.Emulator.Processor.Addressing.Modes;
using static Gamemu.Emulator.Processor.Instructions.Condition;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0xCD, Cycles = 24, Source = ImmediateValue16)]
    
[Instruction(Opcode = 0xC4, Cycles = 12, CyclesAlternate = 24, Source = ImmediateValue16, JumpCondition = NotZero)]
[Instruction(Opcode = 0xCC, Cycles = 12, CyclesAlternate = 24, Source = ImmediateValue16, JumpCondition = Zero)]
[Instruction(Opcode = 0xD4, Cycles = 12, CyclesAlternate = 24, Source = ImmediateValue16, JumpCondition = NotCarry)]
[Instruction(Opcode = 0xDC, Cycles = 12, CyclesAlternate = 24, Source = ImmediateValue16, JumpCondition = Carry)]
public class Call : ReadInstruction
{
    private readonly Register16 _sp;
    private readonly Register16 _pc;
    private readonly MemoryMap _memory;
    private readonly bool? _shouldJump;
    private readonly int _alternateCycles;
        
    public Call(ISource source, [Sp] Register16 sp, [Pc] Register16 pc, MemoryMap memory, bool? shouldJump, int cycles, [Alternate] int alternateCycles) : base(source, cycles)
    {
        _sp = sp;
        _pc = pc;
        _memory = memory;
        _shouldJump = shouldJump;
        _alternateCycles = alternateCycles;
    }

    public override void Execute()
    {
        switch (_shouldJump)
        {
            case null:
                InstructionUtilities.Push(_sp, _memory, _pc.Read());
                InstructionUtilities.Jump(_pc, Source.Read());
                break;
            case true:
                Cycles = _alternateCycles;
                InstructionUtilities.Push(_sp, _memory, _pc.Read());
                InstructionUtilities.Jump(_pc, Source.Read());
                break;
            case false:
                break;
        }
    }
}