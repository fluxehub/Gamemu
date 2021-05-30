using System;
using Gamemu.Emulator;

namespace Gamemu
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new CartridgeFactory("roms/cpu_instrs.gb").MakeCartridge();
            var memory = new MemoryMap(test);
            var cpu = new Emulator.CPU.CPU(memory);
            
            var decompiler = new Decompiler(test);
            var next = 0x491;
            
            while (true)
            {
                next = decompiler.DecompileAt(next);
                Console.ReadLine();
            }
        }
    }
}
