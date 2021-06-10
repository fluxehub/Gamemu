using Gamemu.Emulator.Processor.Addressing;
using static Gamemu.Emulator.Processor.Instructions.Condition;

namespace Gamemu.Emulator.Processor.Instructions
{
        
    [Instruction(Opcode = 0xC9, Cycles = 16)]
    
    [Instruction(Opcode = 0xC0, Cycles = 8, CyclesAlternate = 20, JumpCondition = NotZero)]
    [Instruction(Opcode = 0xC8, Cycles = 8, CyclesAlternate = 20, JumpCondition = Zero)]
    [Instruction(Opcode = 0xD0, Cycles = 8, CyclesAlternate = 20, JumpCondition = NotCarry)]
    [Instruction(Opcode = 0xD8, Cycles = 8, CyclesAlternate = 20, JumpCondition = Carry)]
    public class RET : Instruction
    {
        private readonly Register16 _sp;
        private readonly Register16 _pc;
        private readonly MemoryMap _memory;
        private readonly bool? _shouldJump;
        private readonly int _alternateCycles;
        
        public RET([SP] Register16 sp, [PC] Register16 pc, MemoryMap memory, bool? shouldJump, int cycles, [Alternate] int alternateCycles) : base(cycles)
        {
            _sp = sp;
            _pc = pc;
            _memory = memory;
            _shouldJump = shouldJump;
            _alternateCycles = alternateCycles;
        }

        public override void Execute()
        {
            int address;
            
            switch (_shouldJump)
            {
                case null:
                    address = InstructionUtilities.Pop(_sp, _memory);
                    InstructionUtilities.Jump(_pc, address);
                    break;
                case true:
                    Cycles = _alternateCycles;
                    address = InstructionUtilities.Pop(_sp, _memory);
                    InstructionUtilities.Jump(_pc, address);
                    break;
                case false:
                    break;
            }
        }
    }
    
    [Instruction(Opcode = 0xD9, Cycles = 16)]
    public class RETI : Instruction
    {
        private readonly CPU _cpu;
        private readonly Register16 _sp;
        private readonly Register16 _pc;
        private readonly MemoryMap _memory;
        
        public RETI(CPU cpu, [SP] Register16 sp, [PC] Register16 pc, MemoryMap memory, int cycles) : base(cycles)
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
}