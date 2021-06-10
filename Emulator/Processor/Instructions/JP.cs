using Gamemu.Emulator.Processor.Addressing;
using static Gamemu.Emulator.Processor.Instructions.Condition;

namespace Gamemu.Emulator.Processor.Instructions
{
    [Instruction(Opcode = 0xC3, Cycles = 16, Source = AddressingMode.Immediate16)]
    [Instruction(Opcode = 0xE9, Source = AddressingMode.RegisterHL)]
    
    [Instruction(Opcode = 0xC2, Cycles = 12, CyclesAlternate = 16, Source = AddressingMode.Immediate16, JumpCondition = NotZero)]
    [Instruction(Opcode = 0xCA, Cycles = 12, CyclesAlternate = 16, Source = AddressingMode.Immediate16, JumpCondition = Zero)]
    [Instruction(Opcode = 0xD2, Cycles = 12, CyclesAlternate = 16, Source = AddressingMode.Immediate16, JumpCondition = NotCarry)]
    [Instruction(Opcode = 0xDA, Cycles = 12, CyclesAlternate = 16, Source = AddressingMode.Immediate16, JumpCondition = Carry)]
    public class JP : ReadInstruction
    {
        private readonly Register16 _pc;
        private readonly bool? _shouldJump;
        private readonly int _alternateCycles;
        
        public JP(ISource source, [PC] Register16 pc, bool? shouldJump, int cycles, [Alternate] int alternateCycles) : base(source, cycles)
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
}