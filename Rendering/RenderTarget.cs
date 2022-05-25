using System;

namespace Gamemu.Rendering;

// Actually just a texture but you get
// TODO: When other windows are developed, work out if this is actually needed or not
public abstract class RenderTarget
{
    public IntPtr Handle { get; protected init; }
}