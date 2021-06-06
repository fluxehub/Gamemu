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

var rendererPtr = SDL_CreateRenderer(windowPtr, -1,
    SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

if (rendererPtr == IntPtr.Zero)
    throw new Exception($"Error creating renderer: {SDL_GetError()}");

var texturePtr = SDL_CreateTexture(rendererPtr, SDL_PIXELFORMAT_ABGR8888, (int) SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING,
    160, 144);

if (texturePtr == IntPtr.Zero)
    throw new Exception($"Error creating texture: {SDL_GetError()}");

IntPtr screenPtr = Marshal.AllocHGlobal(160 * 144 * 4);
var screen = new byte[160 * 144 * 4];
SDL_SetRenderDrawColor(rendererPtr, 0xFF, 0xFF, 0xFF, 0xFF);

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

    for (var i = 0; i < (160 * 144 * 4); i += 4)
    {
        Color color = null;
        switch (i / (160 * 4 * 8) % 4)
        {
            case 0:
                color = (i / (8 * 4) % 4) switch
                {
                    0 => black,
                    1 => darkGray,
                    2 => lightGray,
                    3 => white,
                    _ => null
                };
                break;
            case 1:
                color = (i / (8 * 4) % 4) switch
                {
                    1 => black,
                    2 => darkGray, 
                    3 => lightGray,
                    0 => white,
                    _ => null
                };
                break;
            case 2:
                color = (i / (8 * 4) % 4) switch
                {
                    2 => black,
                    3 => darkGray,
                    0 => lightGray,
                    1 => white,
                    _ => null
                };
                break;
            case 3:
                color = (i / (8 * 4) % 4) switch
                {
                    3 => black,
                    0 => darkGray,
                    1 => lightGray,
                    2 => white,
                    _ => null
                };
                break;
        }
            

        screen[i] = color.R;
        screen[i + 1] = color.G;
        screen[i + 2] = color.B;
        screen[i + 3] = 0xFF;
    }
    
    Marshal.Copy(screen, 0, screenPtr, 160 * 144 * 4);
    SDL_UpdateTexture(texturePtr, IntPtr.Zero, screenPtr, 160 * 4);
    SDL_RenderClear(rendererPtr);
    SDL_RenderCopy(rendererPtr, texturePtr, IntPtr.Zero, IntPtr.Zero);
    SDL_RenderPresent(rendererPtr);

    frames++;
    if (lastTime < SDL_GetTicks() - 1000)
    {
        lastTime = SDL_GetTicks();
        current = frames;
        frames = 0;
    }
    
    SDL_SetWindowTitle(windowPtr, $"{test.Title} - {current} FPS");
}

Marshal.FreeHGlobal(screenPtr);
SDL_DestroyTexture(rendererPtr);
SDL_DestroyRenderer(rendererPtr);
SDL_DestroyWindow(windowPtr);
SDL_Quit();

/*
while (true)
{
    next = decompiler.DecompileAt(next);
    Console.ReadLine();
}
*/