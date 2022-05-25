using Gamemu.Emulator.Processor.Addressing.Modes;
using static Gamemu.Emulator.Processor.Instructions.Condition;
using static Gamemu.Emulator.Processor.Addressing.AddressingMode;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0x18, Cycles = 12, Source = ImmediateValue8Signed)]
    
[Instruction(Opcode = 0x20, Cycles = 8, CyclesAlternate = 12, Source = ImmediateValue8Signed, JumpCondition = NotZero)]
[Instruction(Opcode = 0x30, Cycles = 8, CyclesAlternate = 12, Source = ImmediateValue8Signed, JumpCondition = Zero)]
[Instruction(Opcode = 0x28, Cycles = 8, CyclesAlternate = 12, Source = ImmediateValue8Signed, JumpCondition = NotCarry)]
[Instruction(Opcode = 0x38, Cycles = 8, CyclesAlternate = 12, Source = ImmediateValue8Signed, JumpCondition = Carry)]
public class Jr : ReadInstruction
{
    private readonly Register16 _pc;
    private readonly bool? _shouldJump;
    private readonly int _alternateCycles;
        
    public Jr(ISource source, [Pc] Register16 pc, bool? shouldJump, int cycles, [Alternate] int alternateCycles) : base(source, cycles)
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
                InstructionUtilities.Jump(_pc, _pc.Read() + Source.Read());
                break;
            case true:
                Cycles = _alternateCycles;
                InstructionUtilities.Jump(_pc, _pc.Read() + Source.Read());
                break;
            case false:
                break;
        }
    }
}