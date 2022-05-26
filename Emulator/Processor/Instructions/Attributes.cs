using Gamemu.Emulator.Processor.Addressing;

namespace Gamemu.Emulator.Processor.Instructions;

[System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = true)]  
public class InstructionAttribute : System.Attribute
{
    public int Opcode;
    public int Cycles = 4;
    public int CyclesAlternate = 0;
    public int RestartAddress = 0;
    public AddressingMode Source = AddressingMode.None;
    public AddressingMode Dest = AddressingMode.None;
    public AddressingMode Addressable = AddressingMode.None;
    public ConditionType JumpCondition = ConditionType.None;
}

[System.AttributeUsage(System.AttributeTargets.Parameter)]
public class AlternateAttribute : System.Attribute
{
}

[System.AttributeUsage(System.AttributeTargets.Parameter)]
public class PcAttribute : System.Attribute
{
}
    
[System.AttributeUsage(System.AttributeTargets.Parameter)]
public class SpAttribute : System.Attribute
{
}
    
[System.AttributeUsage(System.AttributeTargets.Parameter)]
public class AAttribute : System.Attribute
{
}
    
[System.AttributeUsage(System.AttributeTargets.Parameter)]
public class RestartAddressAttribute : System.Attribute
{
}