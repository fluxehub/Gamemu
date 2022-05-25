using Gamemu.Emulator.Processor.Addressing.Modes;
using static Gamemu.Emulator.Processor.Instructions.Condition;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0xC3, Cycles = 16, Source = ImmediateValue16)]
[Instruction(Opcode = 0xE9, Source = RegisterHl)]
    
[Instruction(Opcode = 0xC2, Cycles = 12, CyclesAlternate = 16, Source = ImmediateValue16, JumpCondition = NotZero)]
[Instruction(Opcode = 0xCA, Cycles = 12, CyclesAlternate = 16, Source = ImmediateValue16, JumpCondition = Zero)]
[Instruction(Opcode = 0xD2, Cycles = 12, CyclesAlternate = 16, Source = ImmediateValue16, JumpCondition = NotCarry)]
[Instruction(Opcode = 0xDA, Cycles = 12, CyclesAlternate = 16, Source = ImmediateValue16, JumpCondition = Carry)]
public class Jp : ReadInstruction
{
    private readonly Register16 _pc;
    private readonly bool? _shouldJump;
    private readonly int _alternateCycles;
        
    public Jp(ISource source, [Pc] Register16 pc, bool? shouldJump, int cycles, [Alternate] int alternateCycles) : base(source, cycles)
    {
        _pc = pc;
        _shouldJump = shouldJump;
        _alternateCycles = alternateCycles;
    }
        
    public override void Execute()
    {
        switch (_shouldJump)
        {
            case null:
                InstructionUtilities.Jump(_pc, Source.Read());
                break;
            case true:
                Cycles = _alternateCycles;
                InstructionUtilities.Jump(_pc, Source.Read());
                break;
            case false:
                break;
        }
    }
}