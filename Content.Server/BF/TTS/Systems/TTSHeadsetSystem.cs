using System.Threading.Tasks;
using Content.Server.Radio;
using Content.Server.Radio.Components;
using Content.Shared.BF.TTS;
using Content.Shared.Radio.Components;
using Robust.Shared.Network;
using Robust.Shared.Player;

namespace Content.Server.BF.TTS.Systems;

/// <summary>
/// Uses for radio tts.
/// </summary>
public sealed class TTSHeadsetSystem : EntitySystem
{
    /// <inheritdoc/>
    [Dependency] private readonly TTSSystem _ttsSystem = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<HeadsetComponent, RadioPlayTts>(OnRadioReceive);
    }

    private async void OnRadioReceive(EntityUid uid, HeadsetComponent comp, RadioPlayTts args)
    {
        if (!TryComp(Transform(uid).ParentUid, out ActorComponent? actor))
        {
            return;
        }

        var effects = args.TtsEffects | TtsEffects.Whisper | TtsEffects.Radio;
        var audioData = await _ttsSystem.GenerateTTS(args.Message, args.Voice, effects);

        if (audioData == null)
        {
            return;
        }

        var ev = new PlayTTSEvent(audioData, GetNetEntity(args.Source), effects.HasFlag(TtsEffects.Whisper));
        RaiseNetworkEvent(ev, actor.PlayerSession.Channel);
    }
    public record RadioPlayTts(string Voice, string Message, TtsEffects TtsEffects, EntityUid Source);
}
