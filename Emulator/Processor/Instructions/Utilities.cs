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

        /// <summary>
        /// Calculates the Zero, Half Carry and Carry flags and sets the Subtraction flag as given. Adds a and b together to calculate.
        /// </summary>
        /// <param name="flags">The current flags register</param>
        /// <param name="a">First value in equation</param>
        /// <param name="b">Second value in equation</param>
        /// <param name="subtractionFlag">Whether the subtraction flag should be set or unset</param>
        /// <param name="doubleWidth">Whether the values are 16-bit (double width) or not</param>
        /// <param name="calculateZero">Whether to set the zero flag or not (used by ADD/SUB16)</param>
        /// <param name="calculateCarry">Whether to set the carry flag or not (used by INC/DEC)</param>
        /// <param name="withCarry">Whether an instruction uses the carry flag in its arithmetic</param>
        public static void SetFlags(FlagsRegister flags, int a, int b, bool subtractionFlag, bool doubleWidth = false, bool calculateZero = true, bool calculateCarry = true, bool withCarry = false)
        {
            flags.ZeroFlag = a + b == 0;
            // The carry flags only care about the high byte of the registers
            if (doubleWidth)
            {
                a >>= 8;
                b >>= 8;
            }
            else
            {
                // If we're using the carry, we need to preserve the upper byte
                if (!withCarry)
                {
                    // ADD SP,r8 just cares about the low byte but it's a 16-bit value
                    // so we need to only include the low byte
                    a &= 0xFF;
                    b &= 0xFF;
                }
            }
            
            // Carried into the 9th bit
            flags.CarryFlag = (a + b) >> 8 != 0;
            
            // Carried into the 5th bit
            // Gonna be honest, I just copied this off stack overflow
            flags.HalfCarryFlag = (((a & 0xF) + (b & 0xF)) & 0x10) != 0;

            flags.SubtractionFlag = subtractionFlag;
        }
    }
}