using System;
using Gamemu.Emulator.Processor.Addressing;

namespace Gamemu.Emulator.Processor
{
    public partial class CPU
    {
        private readonly Register _a, _b, _c, _d, _e, _h, _l;
        private readonly CombinedRegister _af, _bc, _de, _hl;
        private readonly FlagsRegister _f = new();

        private readonly Register16 _sp = new();
        private readonly Register16 _pc = new();
        private readonly MemoryMap _memory;

        public CPU(MemoryMap memoryMap)
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

        public void Tick()
        {
            var opcode = _memory.Read(_pc.Read());
            
            if (opcode == 0xCB)
            {
                _pc.Increment();
                opcode = opcode << 8 | _memory.Read(_pc.Read());
            }

            //var cycles = DecodeAndExecute(opcode);

            _pc.Increment();
        }
    }
}