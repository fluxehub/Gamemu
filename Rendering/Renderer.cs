using System;
using SDL2;

namespace Gamemu.Rendering;

public class Renderer : IDisposable
{
    public IntPtr Handle { get; }

    public Renderer(IntPtr window, SDL.SDL_RendererFlags flags)
    {
        Handle = SDL.SDL_CreateRenderer(window, -1, flags);
        if (Handle == IntPtr.Zero)
            throw new Exception($"Error creating renderer: {SDL.SDL_GetError()}");

        SDL.SDL_SetRenderDrawColor(Handle, 0xFF, 0xFF, 0xFF, 0xFF);
    }

    public void Render(RenderTarget target)
    {
        // TODO: Use HRESULT
        SDL.SDL_RenderClear(Handle);
        SDL.SDL_RenderCopy(Handle, target.Handle, IntPtr.Zero, IntPtr.Zero);
        SDL.SDL_RenderPresent(Handle);
    }

    private void Dispose(bool disposing)
    {
        SDL.SDL_DestroyRenderer(Handle);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Renderer() => Dispose(false);
}