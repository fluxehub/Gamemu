using System;

namespace Gamemu.Emulator.CPU
{
    public partial class CPU
    {
        private CPURegister _a, _b, _c, _d, _e, _h, _l;
        private CPUCombinedRegister _af, _bc, _de, _hl;
        private CPU16Register _sp, _pc;
        private CPUFlagsRegister _f;
        private MemoryBus _memory;

        public CPU(MemoryMap memoryMap)
        {
            _a = new CPURegister();
            _b = new CPURegister();
            _c = new CPURegister();
            _d = new CPURegister();
            _e = new CPURegister();
            _h = new CPURegister();
            _l = new CPURegister();
            _f = new CPUFlagsRegister();

            _sp = new CPU16Register();
            _pc = new CPU16Register();
            
            _af = new CPUCombinedRegister(_a, _f);
            _bc = new CPUCombinedRegister(_b, _c);
            _de = new CPUCombinedRegister(_d, _e);
            _hl = new CPUCombinedRegister(_h, _l);

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
            _memory = new MemoryBus(memoryMap);
            _memory.Address = _pc.Read();
        }

        public void DumpRegisters()
        {
            // TODO: Use logger
            Console.WriteLine($"CPU Registers");
            Console.WriteLine($"AF: 0x{_af.Read():X4}    BC: 0x{_bc.Read():X4}");
            Console.WriteLine($"DE: 0x{_de.Read():X4}    HL: 0x{_hl.Read():X4}");
            Console.WriteLine($"SP: 0x{_sp.Read():X4}    PC: 0x{_pc.Read():X4}");
        }

        public void Tick()
        {
            int opcode = _memory.ReadAddress(_pc.Read());
            
            if (opcode == 0xCB)
            {
                _pc.Increment();
                opcode = opcode << 8 | _memory.ReadAddress(_pc.Read());
            }

            //int cycles = DecodeAndExecute(opcode);
            
            _pc.Increment();
        }
    }
}