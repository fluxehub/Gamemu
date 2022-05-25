namespace Gamemu.Emulator.Processor.Addressing;

public enum AddressingMode
{
    None,
    RegisterA,
    RegisterB,
    RegisterC,
    RegisterD,
    RegisterE,
    RegisterH,
    RegisterL,
    RegisterAf,
    RegisterBc,
    RegisterDe,
    RegisterHl,
    RegisterSp,
    AbsoluteBc,
    AbsoluteDe,
    AbsoluteHl,
    AbsoluteSp,
    AbsoluteHlInc,
    AbsoluteHlDec,
    AbsoluteImmediate,
    ImmediateValue8,
    ImmediateValue8Signed,
    ImmediateValue16,
    IOImmediate,
    IORegisterC
}