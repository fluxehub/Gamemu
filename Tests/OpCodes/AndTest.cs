using Gamemu.Emulator.Processor.Addressing.Modes;
using Gamemu.Emulator.Processor.Instructions.OpCodes;
using Xunit;

namespace Tests.OpCodes;

public class AndTest
{
    [Fact]
    public void And_SourceAndA_ReturnsCorrectResult()
    {
        var a = new MockRegister(0xFF);
        var source = new MockRegister(0xF0);

        var and = new And(a, source, new FlagsRegister(), 0);
        and.Execute();
        
        Assert.Equal(0xF0, a.Value);
    }

    [Fact]
    public void And_ResultZero_SetsCorrectFlags()
    {
        var a = new MockRegister(0xFF);
        var source = new MockRegister(0x00);
        var flags = new FlagsRegister();
        
        new And(a, source, flags, 0).Execute();
        
        Assert.True(flags.ZeroFlag);
        Assert.False(flags.SubtractionFlag);
        Assert.True(flags.HalfCarryFlag);
        Assert.False(flags.CarryFlag);
    }
    
    [Fact]
    public void And_ResultNotZero_SetsCorrectFlags()
    {
        var a = new MockRegister(0xFF);
        var source = new MockRegister(0x01);
        var flags = new FlagsRegister();
        
        new And(a, source, flags, 0).Execute();
        
        Assert.False(flags.ZeroFlag);
        Assert.False(flags.SubtractionFlag);
        Assert.True(flags.HalfCarryFlag);
        Assert.False(flags.CarryFlag);
    }
}