using Gamemu.Emulator.Processor.Addressing;
using static Gamemu.Emulator.Processor.Instructions.Condition;

namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0x18, Cycles = 12, Source = AddressingMode.ImmediateSigned)]
    
    [Instruction(Opcode = 0x20, Cycles = 8, CyclesAlternate = 12, Source = AddressingMode.ImmediateSigned, JumpCondition = NotZero)]
    [Instruction(Opcode = 0x30, Cycles = 8, CyclesAlternate = 12, Source = AddressingMode.ImmediateSigned, JumpCondition = Zero)]
    [Instruction(Opcode = 0x28, Cycles = 8, CyclesAlternate = 12, Source = AddressingMode.ImmediateSigned, JumpCondition = NotCarry)]
    [Instruction(Opcode = 0x38, Cycles = 8, CyclesAlternate = 12, Source = AddressingMode.ImmediateSigned, JumpCondition = Carry)]
    public class JR : ReadInstruction
    {
        private readonly Register16 _pc;
        private readonly bool? _shouldJump;
        private readonly int _alternateCycles;
        
        public JR(ISource source, [PC] Register16 pc, bool? shouldJump, int cycles, [Alternate] int alternateCycles) : base(source, cycles)
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
}