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

        var ld = new Ld(source, dest, 0);
        ld.Execute();
        
        Assert.Equal(1, dest.Value);
    }

    [Fact]
    public void LdWithSpAdd_SourceToDest_SavesValue()
    {
        var source = new MockAddressable(1);
        var dest = new MockAddressable(0);
        var sp = new MockRegister16(1);
        var flags = new FlagsRegister();
        
        var ld = new LdWithSpAdd(source, dest, sp, flags, 0);
        ld.Execute();
        
        Assert.Equal(2, dest.Value);
    }

    [Fact]
    public void LdWithSpAdd_SourceToDest_SetsCorrectFlags()
    {
        // TODO: Test flags
    }
}