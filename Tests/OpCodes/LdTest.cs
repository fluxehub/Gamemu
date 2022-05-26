using Gamemu.Emulator.Processor.Addressing.Modes;
using Gamemu.Emulator.Processor.Instructions.OpCodes;
using Xunit;

namespace Tests.OpCodes;

public class LdTest
{
    [Fact]
    public void Ld_SourceToDest_SavesValue()
    {
        var source = new MockAddressable(1);
        var dest = new MockAddressable(0);

        new Ld(source, dest, 0).Execute();
        
        Assert.Equal(1, dest.Value);
    }

    [Fact]
    public void LdWithSpAdd_SourceToDest_SavesValue()
    {
        var source = new MockAddressable(1);
        var dest = new MockAddressable(0);
        var sp = new MockRegister16(1);
        var flags = new FlagsRegister();
        
        new LdWithSpAdd(source, dest, sp, flags, 0).Execute();
        
        Assert.Equal(2, dest.Value);
    }

    [Fact]
    public void LdWithSpAdd_SourceToDest_SetsCorrectFlags()
    {
        var source = new MockAddressable(0x1);
        var dest = new MockAddressable(0);
        var sp = new MockRegister16(0xF);
        var flags = new FlagsRegister();
        
        new LdWithSpAdd(source, dest, sp, flags, 0).Execute();
        
        Assert.False(flags.ZeroFlag);
        Assert.False(flags.SubtractionFlag);
        Assert.True(flags.HalfCarryFlag);
        Assert.False(flags.CarryFlag);
        
        source.Write(0b1000_0000);
        sp.Write(0b1000_1000);
        
        new LdWithSpAdd(source, dest, sp, flags, 0).Execute();

        Assert.False(flags.ZeroFlag);
        Assert.False(flags.SubtractionFlag);
        Assert.False(flags.HalfCarryFlag);
        Assert.True(flags.CarryFlag);
    }
}