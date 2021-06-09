using Gamemu.Emulator.Processor.Addressing;

namespace Gamemu.Emulator.Processor.Instructions
{
    public class JP : ReadInstruction
    {
        private readonly Register16 _pc;
        private readonly bool? _shouldJump;
        private readonly int _alternateCycles;
        
        JP(ISource source, [PC] Register16 pc, bool? shouldJump, int cycles, [Alternate] int alternateCycles) : base(source, cycles)
        {
            _pc = pc;
            _shouldJump = shouldJump;
            _alternateCycles = alternateCycles;
        }

        private void Jump()
        {
            // TODO: Check when PC is incremented and whether this needs to be subtracted
            _pc.Write(Source.Read() - 1);
        }

        public override void Execute()
        {
            switch (_shouldJump)
            {
                case null:
                    Jump();
                    break;
                case true:
                    Cycles = _alternateCycles;
                    Jump();
                    break;
                case false:
                    break;
            }
        }
    }
}