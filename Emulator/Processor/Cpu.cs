using System;
using Gamemu.Emulator.Processor.Addressing.Modes;

namespace Gamemu.Emulator.Processor;

public partial class Cpu
{
    private readonly Register _a, _b, _c, _d, _e, _h, _l;
    private readonly CombinedRegister _af, _bc, _de, _hl;
    private readonly FlagsRegister _f = new();

    private readonly Register16 _sp = new();
    private readonly Register16 _pc = new();
    private readonly MemoryMap _memory;
        
    // Needs to be public for STOP/HALT
    public ExecutionStatus ExecutionStatus { get; set; }

    // Needs to be public for EI/DI/RETI
    public InterruptStatus InterruptStatus { get; set; }

    public Cpu(MemoryMap memoryMap)
    {
        _a = new Register();
        _b = new Register();
        _c = new Register();
        _d = new Register();
        _e = new Register();
        _h = new Register();
        _l = new Register();

        _af = new CombinedRegister(_a, _f);
        _bc = new CombinedRegister(_b, _c);
        _de = new CombinedRegister(_d, _e);
        _hl = new CombinedRegister(_h, _l);

        // Set initial values
        _a.Write(0x0001);
        _bc.Write(0x0013);
        _de.Write(0x00D8);
        _hl.Write(0x014D);
        _sp.Write(0xFFFE);
        _pc.Write(0x0100);

        _f.ZeroFlag = true;
        _f.SubtractionFlag = false;
        _f.HalfCarryFlag = true;
        _f.CarryFlag = true;
        _memory = memoryMap;
            
        CreateInstructionTable();
    }

    public void DumpRegisters()
    {
        // TODO: Use logger
        Console.WriteLine($"CPU Registers");
        Console.WriteLine($"AF: 0x{_af.Read():X4}    BC: 0x{_bc.Read():X4}");
        Console.WriteLine($"DE: 0x{_de.Read():X4}    HL: 0x{_hl.Read():X4}");
        Console.WriteLine($"SP: 0x{_sp.Read():X4}    PC: 0x{_pc:X4}");
    }

    private void FetchDecodeExecute()
    {
        var opcode = _memory[_pc.Read()];
        var instructionTable = _instructionTable;
            
        if (opcode == 0xCB)
        {
            instructionTable = _cbInstructionTable;
            _pc.Increment();
            opcode = _memory[_pc.Read()];
        }

        var instruction = instructionTable[opcode];
        instruction.Execute();
            
        _pc.Increment();
    }

    public void Tick()
    {
        if (ExecutionStatus == ExecutionStatus.Stopped) return;
            
        FetchDecodeExecute();
    }
}