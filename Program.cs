using System;
using Gamemu.Emulator;
using Gamemu.Rendering;

var test = new CartridgeFactory("roms/cpu_instrs.gb").MakeCartridge();
var memory = new MemoryMap(test);
//var cpu = new Gamemu.Emulator.Processor.CPU(memory);

var decompiler = new Decompiler(test);
var next = 0x491;

var palette = Palette.FromPaletteImage("Palettes/crimson-1x.png");

/*
while (true)
{
    next = decompiler.DecompileAt(next);
    Console.ReadLine();
}
*/