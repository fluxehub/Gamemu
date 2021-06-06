using System;
using System.Runtime.InteropServices;
using Gamemu.Emulator;
using Gamemu.Rendering;
using static SDL2.SDL;

var test = new CartridgeFactory("roms/cpu_instrs.gb").MakeCartridge();
var memory = new MemoryMap(test);
//var cpu = new Gamemu.Emulator.Processor.CPU(memory);

var decompiler = new Decompiler(test);
var next = 0x491;

var palette = Palette.FromPaletteImage("Palettes/mist-gb-1x.png");

if (SDL_Init(SDL_INIT_VIDEO | SDL_INIT_AUDIO) < 0)
    throw new Exception($"Error initializing SDL: {SDL_GetError()}");

var windowPtr = SDL_CreateWindow("Gamemu", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 160 * 2, 144 * 2,
    SDL_WindowFlags.SDL_WINDOW_SHOWN);

if (windowPtr == IntPtr.Zero)
    throw new Exception($"Error creating SDL window: {SDL_GetError()}");

var surfacePtr = SDL_GetWindowSurface(windowPtr);
var surface = Marshal.PtrToStructure<SDL_Surface>(surfacePtr);
var white = palette.GetColor(PaletteColor.White);
if (SDL_FillRect(surfacePtr, IntPtr.Zero, SDL_MapRGB(surface.format, white.R, white.G, white.B)) < 0)
    throw new Exception($"Error filling rectangle: {SDL_GetError()}");

SDL_UpdateWindowSurface(windowPtr);

var quit = false;
while (!quit)
{
    while (SDL_PollEvent(out var e) != 0)
    {
        if (e.type == SDL_EventType.SDL_QUIT)
            quit = true;
    }
}

SDL_FreeSurface(surfacePtr);
SDL_Quit();

/*
while (true)
{
    next = decompiler.DecompileAt(next);
    Console.ReadLine();
}
*/