using System;
using Gamemu.Emulator.Processor.Addressing;

namespace Gamemu.Emulator.Processor
{
    public partial class CPU
    {
        private readonly Register _a, _b, _c, _d, _e, _h, _l;
        private readonly CombinedRegister _af, _bc, _de, _hl;

        // Required to be public in order for instructions to access
        public FlagsRegister Flags { get; }
        public Register16 StackPointer { get; set; }
        public int ProgramCounter { get; set; }
        public MemoryMap Memory { get; }

        public CPU(MemoryMap memoryMap)
        {
            _a = new Register();
            _b = new Register();
            _c = new Register();
            _d = new Register();
            _e = new Register();
            _h = new Register();
            _l = new Register();
            Flags = new FlagsRegister();

            StackPointer = new Register16();

            _af = new CombinedRegister(_a, Flags);
            _bc = new CombinedRegister(_b, _c);
            _de = new CombinedRegister(_d, _e);
            _hl = new CombinedRegister(_h, _l);

            // Set initial values
             _a.Write(0x0001);
            _bc.Write(0x0013);
            _de.Write(0x00D8);
            _hl.Write(0x014D);
            StackPointer.Write(0xFFFE);
            ProgramCounter = 0x0100;

            Flags.ZeroFlag = true;
            Flags.SubtractionFlag = false;
            Flags.HalfCarryFlag = true;
            Flags.CarryFlag = true;
            Memory = memoryMap;
            
            CreateInstructionTable();
        }

        public void DumpRegisters()
        {
            // TODO: Use logger
            Console.WriteLine($"CPU Registers");
            Console.WriteLine($"AF: 0x{_af.Read():X4}    BC: 0x{_bc.Read():X4}");
            Console.WriteLine($"DE: 0x{_de.Read():X4}    HL: 0x{_hl.Read():X4}");
            Console.WriteLine($"SP: 0x{StackPointer.Read():X4}    PC: 0x{ProgramCounter:X4}");
        }

        public void Tick()
        {
            var opcode = Memory.Read(ProgramCounter);
            
            if (opcode == 0xCB)
            {
                ProgramCounter++;
                opcode = opcode << 8 | Memory.Read(ProgramCounter);
            }

            //var cycles = DecodeAndExecute(opcode);

            ProgramCounter++;
        }
    }
}