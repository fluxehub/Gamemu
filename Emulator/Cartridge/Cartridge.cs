using System;
using System.Text;

namespace Gamemu.Emulator
{
    public class Cartridge : Memory
    {
        public string Title { get; set; }
        
        private readonly byte[] _data;
        
        // Will be used by mapping cartridges
        private readonly int _romBanks;

        private static readonly int[] _ramBanksTable = {0, 0, 1, 4, 16, 8};
        private readonly int _ramBanks;

        public Cartridge(byte[] data)
        {
            _data = data;
            var titleBuilder = new StringBuilder();

            var titleLength = _data[0x014B] == 0x33 ? 11 : 16;
            // Read cartridge title
            for (var i = 0; i < titleLength; ++i)
            {
                // Get character at title offset
                var character = (char) _data[0x0134 + i];

                if (character == '\0')
                    break;

                titleBuilder.Append(character);
            }

            // Number of rom banks is 2^(rom size code + 1)
            _romBanks = (int) Math.Pow(2, _data[0x0148] + 1);
            _ramBanks = _ramBanksTable[_data[0x0149]];
            
            Title = titleBuilder.ToString();
        }

        protected override int Read(int address)
        {
            return _data[address];
        }

        protected override void Write(int address, int value)
        {
            throw new InvalidOperationException("Attempt to write to ROM-only cartridge");
        }
    }
}