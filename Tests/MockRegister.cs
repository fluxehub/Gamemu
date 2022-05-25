using Gamemu.Emulator.Processor.Addressing.Modes;

namespace Tests;

public class MockRegister : Register
{
    public byte Value { get; set; }
    
    public MockRegister(byte value)
    {
        Value = value;
    }

    public override int Read()
    {
        return Value;
    }
    
    public override void Write(int value)
    {
        Value = (byte) value;
    }
}

public class MockRegister16 : Register16
{
    public int Value { get; set; }
    
    public MockRegister16(int value)
    {
        Value = value;
    }
    
    public override int Read()
    {
        return Value;
    }
    
    public override void Write(int value)
    {
        Value = value;
    }
}