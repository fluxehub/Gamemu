using System;
using System.Runtime.InteropServices;
using SDL2;

namespace Gamemu.Rendering;

public class Screen : RenderTarget, IDisposable
{
    // Screen buffer is always 160 x 144 (resolution) x 4 (bytes per pixel)
    private const int BufferSize = 160 * 144 * 4;
        
    // 3 unmanaged resources woo
    private readonly IntPtr _formatHandle;
    private readonly IntPtr _bufferHandle;
        
    public Screen(Renderer renderer)
    {
        Handle = SDL.SDL_CreateTexture(renderer.Handle, SDL.SDL_PIXELFORMAT_ABGR8888,
            (int) SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING, 160, 144);
            
        if (Handle == IntPtr.Zero)
            throw new Exception($"Error creating texture: {SDL.SDL_GetError()}");
            
        // Save pixel format so we can use it for SDL_MapRGB calls
        _formatHandle = SDL.SDL_AllocFormat(SDL.SDL_PIXELFORMAT_ABGR8888);

        // SDL_UpdateTexture needs a pointer to a buffer unfortunately
        // so allocate some space for the buffer
        _bufferHandle = Marshal.AllocHGlobal(BufferSize);
    }

    public unsafe void Blit(Color[] viewport)
    {
        var buffer = new Span<uint>(_bufferHandle.ToPointer(), viewport.Length);

        for (var i = 0; i < viewport.Length; i++)
        {
            var (r, g, b) = viewport[i];
            buffer[i] = SDL.SDL_MapRGB(_formatHandle, r, g, b);
        }

        SDL.SDL_UpdateTexture(Handle, IntPtr.Zero, _bufferHandle, 160 * 4);
    }

    private void Dispose(bool disposing)
    {
        SDL.SDL_DestroyRenderer(Handle);
        SDL.SDL_FreeFormat(_formatHandle);
        Marshal.FreeHGlobal(_bufferHandle);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Screen() => Dispose(false);
}