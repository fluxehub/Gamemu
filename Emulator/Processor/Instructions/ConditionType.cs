using System;
using Gamemu.Emulator.Processor.Addressing.Modes;

namespace Gamemu.Emulator.Processor.Instructions;

public enum ConditionType
{
    Zero,
    NotZero,
    Carry,
    NotCarry,
    None
}

public abstract class Condition
{
    protected readonly FlagsRegister flags;
    
    protected Condition(FlagsRegister flags)
    {
        this.flags = flags;
    }
    
    // There's no way to define an abstract implicit operator :/
    public static implicit operator bool(Condition condition)
    {
        throw new NotSupportedException("Cannot convert a base condition to bool");
    }
}

public class ZeroCondition : Condition
{
    public ZeroCondition(FlagsRegister flags) : base(flags)
    {
    }
    
    public static implicit operator bool(ZeroCondition condition)
    {
        return condition.flags.ZeroFlag;
    }
}

public class NotZeroCondition : Condition
{
    public NotZeroCondition(FlagsRegister flags) : base(flags)
    {
    }
    
    public static implicit operator bool(NotZeroCondition condition)
    {
        return !condition.flags.ZeroFlag;
    }
}

public class CarryCondition : Condition
{
    public CarryCondition(FlagsRegister flags) : base(flags)
    {
    }
    
    public static implicit operator bool(CarryCondition condition)
    {
        return condition.flags.CarryFlag;
    }
}

public class NotCarryCondition : Condition
{
    public NotCarryCondition(FlagsRegister flags) : base(flags)
    {
    }
    
    public static implicit operator bool(NotCarryCondition condition)
    {
        return !condition.flags.CarryFlag;
    }
}