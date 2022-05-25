using Gamemu.Emulator.Processor.Addressing;
using Gamemu.Emulator.Processor.Addressing.Modes;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0xC5, Cycles = 16, Source = AddressingMode.RegisterBc)]
[Instruction(Opcode = 0xD5, Cycles = 16, Source = AddressingMode.RegisterDe)]
[Instruction(Opcode = 0xE5, Cycles = 16, Source = AddressingMode.RegisterHl)]
[Instruction(Opcode = 0xF5, Cycles = 16, Source = AddressingMode.RegisterAf)]
public class Push : ReadInstruction
{
    private readonly MemoryMap _memory;
    private readonly Register16 _sp;
        
    public Push(ISource source, [Sp] Register16 sp, MemoryMap memory, int cycles) : base(source, cycles)
    {
        _memory = memory;
        _sp = sp;
    }

    public override void Execute()
    {
        InstructionUtilities.Push(_sp, _memory, Source.Read());
    }
}