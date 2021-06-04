using Gamemu.Emulator.Processor.Addressing;

namespace Gamemu.Emulator.Processor.Instructions
{
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = true)]  
    public class InstructionAttribute : System.Attribute
    {
        public int Opcode;
        public int Cycles = 4;
        public int CyclesAlternate = 0;
        public AddressingMode Source = AddressingMode.None;
        public AddressingMode Dest = AddressingMode.None;
        public AddressingMode Addressable = AddressingMode.None;
        public int RestartAddress = 0;
    }

    [System.AttributeUsage(System.AttributeTargets.Parameter)]
    public class AlternateAttribute : System.Attribute
    {
    }

    [System.AttributeUsage(System.AttributeTargets.Parameter)]
    public class PCAttribute : System.Attribute
    {
    }
    
    [System.AttributeUsage(System.AttributeTargets.Parameter)]
    public class SPAttribute : System.Attribute
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
}