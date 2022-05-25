namespace Gamemu.Emulator.Processor;

public enum InterruptStatus
{
    Disabled,
    Pending, // EI does not enable interrupts until the cycle *after* it executes
    Enabled
}