using Robust.Shared.GameStates;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Shared.BF.TTS;

/// <summary>
/// Apply TTS for entity chat say messages
/// </summary>
[RegisterComponent, NetworkedComponent]
// ReSharper disable once InconsistentNaming
public sealed partial class TTSComponent : Component
{
    /// <summary>
    /// Prototype of used voice for TTS.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite)]
    [DataField("voice", customTypeSerializer: typeof(PrototypeIdSerializer<TTSVoicePrototype>))]
    public string? VoicePrototypeId { get; set; }

    [ViewVariables(VVAccess.ReadWrite)]
    [DataField("effects")]
    public TtsEffects Effects = TtsEffects.None;
}

[Flags]
public enum TtsEffects : ushort
{
    None,
    Robot = 1,
    Whisper = 0x1 << 1,
    Announce = 0x1 << 2,
    Radio = 0x1 << 3,
}
