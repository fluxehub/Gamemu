using Gamemu.Emulator.Processor.Addressing;
using Gamemu.Emulator.Processor.Addressing.Modes;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0xC1, Cycles = 12, Dest = AddressingMode.RegisterBc)]
[Instruction(Opcode = 0xD1, Cycles = 12, Dest = AddressingMode.RegisterDe)]
[Instruction(Opcode = 0xE1, Cycles = 12, Dest = AddressingMode.RegisterHl)]
[Instruction(Opcode = 0xF1, Cycles = 12, Dest = AddressingMode.RegisterAf)]
public class Pop : WriteInstruction
{
    private readonly MemoryMap _memory;
    private readonly Register16 _sp;
        
    public Pop(IDest dest, [Sp] Register16 sp, MemoryMap memory, int cycles) : base(dest, cycles)
    {
        _memory = memory;
        _sp = sp;
    }

    public override void Execute()
    {
        // Write the stack value to the registers
        Dest.Write(InstructionUtilities.Pop(_sp, _memory));
    }
}