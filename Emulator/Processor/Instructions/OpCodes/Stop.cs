using System;

namespace Gamemu.Emulator.Processor.Instructions.OpCodes;

[Instruction(Opcode = 0x10)]
public class Stop : Instruction
{
    private readonly CPU _cpu;
        
    public Stop(CPU cpu, int cycles) : base(cycles)
    {
        _cpu = cpu;
    }

    public override void Execute()
    {
        _cpu.ExecutionStatus = ExecutionStatus.Stopped;
        Console.WriteLine("STOP called, stopping CPU");
    }
}