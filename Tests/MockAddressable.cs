using Gamemu.Emulator;

namespace Tests;

public class MockAddressable : IAddressable
{
    public int Value { get; set; }
    
    public MockAddressable(int value)
    {
        Value = value;
    }

    public int Read()
    {
        return Value;
    }
    
    public void Write(int value)
    {
        Value = value;
    }
}