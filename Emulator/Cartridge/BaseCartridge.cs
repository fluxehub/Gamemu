using System;
using System.Text;

namespace Gamemu.Emulator.Cartridge;

public abstract class BaseCartridge : Memory
{
    public string Title { get; set; }
    protected readonly byte[] _data;

    private static readonly int[] RamBanksTable = {0, 0, 1, 4, 16, 8};

    protected readonly int _ramBanks;
    protected readonly int _romBanks;

    protected bool _hasRam = false;
    protected bool _hasBattery = false;
    protected bool _ramEnabled = false;
    protected bool _hasRtc = false;

    protected BaseCartridge(byte[] data)
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
        _ramBanks = RamBanksTable[_data[0x0149]];
            
        Title = titleBuilder.ToString();
    }
}