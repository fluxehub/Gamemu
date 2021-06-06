using System;
using System.IO;
using System.Runtime.InteropServices;
using Gamemu.Emulator;
using Gamemu.Rendering;
using static SDL2.SDL;

var test = new CartridgeFactory("roms/pokemon.gb").MakeCartridge();
var memory = new MemoryMap(test);
//var cpu = new Gamemu.Emulator.Processor.CPU(memory);

var decompiler = new Decompiler(test);
var next = 0x491;

var paletteFiles = Directory.GetFiles("Palettes/");

var p = 0;
var palette = Palette.FromPaletteImage(paletteFiles[p]);

if (SDL_Init(SDL_INIT_VIDEO | SDL_INIT_AUDIO) < 0)
    throw new Exception($"Error initializing SDL: {SDL_GetError()}");

var windowPtr = SDL_CreateWindow("Gamemu", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 160 * 2, 144 * 2,
    SDL_WindowFlags.SDL_WINDOW_SHOWN);

if (windowPtr == IntPtr.Zero)
    throw new Exception($"Error creating SDL window: {SDL_GetError()}");

using var renderer = new Renderer(windowPtr, SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
using var screen = new Screen(renderer);

var colors = new Color[160 * 144];

var lastTime = SDL_GetTicks();
uint current = 0;
uint frames = 0;

var quit = false;
while (!quit)
{
    while (SDL_PollEvent(out var e) != 0)
    {
        switch (e.type)
        {
            case SDL_EventType.SDL_QUIT:
                quit = true;
                break;
            case SDL_EventType.SDL_KEYDOWN:
                if (e.key.keysym.sym == SDL_Keycode.SDLK_p)
                {
                    p++;
                    p = p == paletteFiles.Length ? 0 : p;
                    palette = Palette.FromPaletteImage(paletteFiles[p]);
                }
                break;
        }
    }
    
    var white = palette.GetColor(PaletteColor.White);
    var lightGray = palette.GetColor(PaletteColor.LightGray);
    var darkGray = palette.GetColor(PaletteColor.DarkGray);
    var black = palette.GetColor(PaletteColor.Black);

    for (var i = 0; i < (160 * 144); i++)
    {
        Color color = (i / (160 * 8) % 4) switch
        {
            0 => (i / 8 % 4) switch
            {
                0 => black,
                1 => darkGray,
                2 => lightGray,
                3 => white,
                _ => null
            },
            1 => (i / (8) % 4) switch
            {
                1 => black,
                2 => darkGray,
                3 => lightGray,
                0 => white,
                _ => null
            },
            2 => (i / (8) % 4) switch
            {
                2 => black,
                3 => darkGray,
                0 => lightGray,
                1 => white,
                _ => null
            },
            3 => (i / (8) % 4) switch
            {
                3 => black,
                0 => darkGray,
                1 => lightGray,
                2 => white,
                _ => null
            },
            _ => null
        };

        colors[i] = color;
    }
    
    screen.Blit(colors);
    renderer.Render(screen);

    frames++;
    if (lastTime < SDL_GetTicks() - 1000)
    {
        lastTime = SDL_GetTicks();
        current = frames;
        frames = 0;
    }
    
    SDL_SetWindowTitle(windowPtr, $"{test.Title} - {current} FPS");
}

SDL_DestroyWindow(windowPtr);
SDL_Quit();

/*
while (true)
{
    next = decompiler.DecompileAt(next);
    Console.ReadLine();
}
*/