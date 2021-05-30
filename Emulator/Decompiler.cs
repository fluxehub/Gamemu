using System;
using System.Collections.Generic;

namespace Gamemu.Emulator
{
    public class Decompiler
    {
        // Decoding logic from https://gb-archive.github.io/salvage/decoding_gbz80_opcodes/Decoding%20Gamboy%20Z80%20Opcodes.html
        private static string[] _r = { "B", "C", "D", "E", "H", "L", "(HL)", "A" };
        private static string[] _rp = { "BC", "DE", "HL", "SP" };
        private static string[] _rp2 = { "BC", "DE", "HL", "AF" };
        private static string[] _cc = { "NZ", "Z", "NC", "C" };
        private static string[] _alu = { "ADD A,", "ADC A,", "SUB", "SBC A,", "AND", "XOR", "OR", "CP" };
        private static string[] _rot = { "RLC", "RRC", "RL", "RR", "SLA", "SRA", "SWAP", "SRL" };

        private Cartridge _cartridge;
        private int _pc;

        public Decompiler(Cartridge cartridge)
        {
            _cartridge = cartridge;
        }

        private int GetImmediate()
        {
            _pc++;
            return _cartridge.Read(_pc);
        }

        private int GetImmediate16()
        {
            _pc++;
            var low = _cartridge.Read(_pc);
            _pc++;
            var high = _cartridge.Read(_pc);
            return high << 8 | low;
        }

        // TODO: Translate displacement byte to effective address
        public string Decode(int opcode)
        {
            var cb = opcode == 0xCB;
            if (cb)
            {
                _pc++;
                opcode = _cartridge.Read(_pc);
            }

            var x = opcode >> 6;
            var y = opcode >> 3 & 7;
            var z = opcode & 7;
            var p = opcode >> 4 & 3;
            var q = opcode >> 3 & 1;
            
            if (!cb)
            {
                switch (x)
                {
                case 0:
                    switch (z)
                    {
                    case 0:
                        if (y >= 4)
                        {
                            return $"JR {_cc[y - 4]}, ${GetImmediate():X2}";
                        }

                        switch (y)
                        {
                        case 0:
                            return "NOP";
                        case 1:
                            return $"LD (${GetImmediate16():X4}), SP";
                        case 2:
                            _pc++;
                            return "STOP";
                        case 3:
                            return $"JR ${GetImmediate():X2}";
                        }
                        break;
                    case 1:
                        if (q == 0)
                            return $"LD {_rp[p]}, {GetImmediate16()}";
                        else
                            return $"ADD HL, {_rp[p]}";
                    case 2:
                        if (q == 0)
                        {
                            switch (p)
                            {
                            case 0:
                                return "LD A, (BC)";
                            case 1:
                                return "LD A, (DE)";
                            case 2:
                                return "LD A, (HL+)";
                            case 3:
                                return "LD A, (HL-)";
                            }
                        }
                        else
                        {
                            switch (p)
                            {
                            case 0:
                                return "LD (BC), A";
                            case 1:
                                return "LD (DE), A";
                            case 2:
                                return "LD (HL+), A";
                            case 3:
                                return "LD (HL-), A";
                            }
                        }
                        break;
                    case 3:
                        if (q == 0)
                            return $"INC {_rp[p]}";
                        else
                            return $"DEC {_rp[p]}";
                    case 4:
                        return $"INC {_r[y]}";
                    case 5:
                        return $"DEC {_r[y]}";
                    case 6:
                        return $"LD {_r[y]}, {GetImmediate()}";
                    case 7:
                        switch (y)
                        {
                        case 0:
                            return "RLCA";
                        case 1:
                            return "RRCA";
                        case 2:
                            return "RLA";
                        case 3:
                            return "RRA";
                        case 4:
                            return "DAA";
                        case 5:
                            return "CPL";
                        case 6:
                            return "SCF";
                        case 7:
                            return "CCF";
                        }
                        break;
                    default:
                        break;
                    }
                    break;
                case 1:
                    if (z == 6 && y == 6)
                    {
                        return "HALT";
                    }
                    else
                    {
                        return $"LD {_r[y]}, {_r[z]}";
                    }
                case 2:
                    return $"{_alu[y]} {_r[z]}";
                case 3:
                    switch (z)
                    {
                    case 0:
                        if (y < 4)
                            return $"RET {_cc[y]}";
                        
                        switch (y)
                        {
                        case 4:
                            return $"LD (${0xFF00 + GetImmediate():X4}), A";
                        case 5:
                            return $"ADD SP, {GetImmediate()}";
                        case 6:
                            return $"LD A, (${0xFF00 + GetImmediate():X4})";
                        case 7:
                            return $"LD HL, SP + {GetImmediate()}";
                        }
                        break;
                    case 1:
                        if (q == 0)
                        {
                            return $"POP {_rp2[p]}";
                        }
                        else
                        {
                            switch (p)
                            {
                            case 0:
                                return "RET";
                            case 1:
                                return "RETI";
                            case 2:
                                return "JP HL";
                            case 3:
                                return "LD SP, HL";
                            }
                        }
                        break;
                    case 2:
                        if (y < 4)
                            return $"JP {_cc[y]}, ${GetImmediate16():X4}";

                        switch (y)
                        {
                        case 4:
                            return $"LD (${0xFF00} + C), A";
                        case 5:
                            return $"LD (${GetImmediate16():X4}), A";
                        case 6:
                            return $"LD A, (${0xFF00} + C)";
                        case 7:
                            return $"LD A, (${GetImmediate16():X4})";
                        }
                        break;
                    case 3:
                        if (y == 0)
                            return $"JP ${GetImmediate16():X4}";
                        else if (y == 6)
                            return "DI";
                        else if (y == 7)
                            return "EI";
                        break;
                    case 4:
                        if (y < 4)
                            return $"CALL {_cc[y]}, ${GetImmediate16():X4}";
                        break;
                    case 5:
                        if (q == 0)
                            return $"PUSH {_rp2[p]}";
                        else
                            if (p == 0)
                                return $"CALL ${GetImmediate16():X4}";
                        
                        break;
                    case 6:
                        return $"{_alu[y]} {GetImmediate()}";
                    case 7:
                        return $"RST {y * 8}";
                    }
                    break;
                }
            }
            else
            {
                switch (x)
                {
                case 0:
                    return $"{_rot[y]} {_r[z]}";
                case 1:
                    return $"BIT {y}, {_r[z]}";
                case 2:
                    return $"RES {y}, {_r[z]}";
                case 3:
                    return $"SET {y}, {_r[z]}";
                }
            }

            return "-- --";
        }

        public string GetInstructionString(int address, int length)
        {
            var instructionBytes = new List<int>();
            
            for (var i = 0; i < length; i++)
            {
                instructionBytes.Add(_cartridge.Read(address + i));
            }

            switch (length)
            {
            case 1:
                return $"      {instructionBytes[0]:X2}";
            case 2:
                return $"   {instructionBytes[0]:X2} {instructionBytes[1]:X2}";
            case 3:
                return $"{instructionBytes[0]:X2} {instructionBytes[1]:X2} {instructionBytes[2]:X2}";
            }
            return null;
        }

        public int DecompileAt(int address, int lines = 6)
        {
            _pc = address;
            int nextInstructionAddr = 0;
            for (var i = 0; i < lines; ++i)
            {
                if (i == 1)
                    nextInstructionAddr = _pc;

                var _pcBeforeDecode = _pc;
                int opcode = _cartridge.Read(_pc);
                string decodedInstruction = Decode(opcode);
                string instructionBytes = GetInstructionString(_pcBeforeDecode, (_pc - _pcBeforeDecode) + 1);
                if (i == 0)
                    Console.WriteLine($">>> ${_pc:X4}: {instructionBytes} {decodedInstruction}");
                else
                    Console.WriteLine($"    ${_pc:X4}: {instructionBytes} {decodedInstruction}");
                
                _pc++;
            }

            return nextInstructionAddr;
        }
    }
}