using Gamemu.Emulator.Processor.Addressing;

namespace Gamemu.Emulator.Processor.Instructions
{
    public static class InstructionUtilities
    {
        /// <summary>
        /// Pushes a value onto the stack
        /// </summary>
        /// <param name="stackPointer">Current stack pointer</param>
        /// <param name="memory">Memory map</param>
        /// <param name="value">Value to push</param>
        public static void Push(Register16 stackPointer, MemoryMap memory, int value)
        {
            // Push onto the next address (stack is decrementing)
            var address = stackPointer.Read() - 1;
            
            // Value is always 16-bit, split and push to stack
            memory[address] =  value >> 8;
            memory[address - 1] = value & 0xff;
            
            // Set the stack pointer to top of stack
            stackPointer.Write(address - 1);
        }
        
        /// <summary>
        /// Pops a value off the stack and returns it
        /// </summary>
        /// <param name="stackPointer">Current stack pointer</param>
        /// <param name="memory">Memory map</param>
        /// <returns>The top value from the stack</returns>
        public static int Pop(Register16 stackPointer, MemoryMap memory)
        {
            var address = stackPointer.Read();
            
            // Get the low and high bytes from the stack
            var low = memory[address];
            var high = memory[address + 1];
            
            // Set the stack pointer to top of stack
            stackPointer.Write(address + 2);
            
            // Return the 16-bit value
            return (high << 8) | low;
        }
        
        /// <summary>
        /// Writes address - 1 to the program counter
        /// </summary>
        /// <param name="programCounter">Current program counter</param>
        /// <param name="address">Address to jump to</param>
        public static void Jump(Register16 programCounter, int address)
        {
            // Technically opcodes without jumps should increment PC by one
            // but it's easier to just increment in a loop and subtract 1 here
            programCounter.Write(address - 1);
        }
    }
}