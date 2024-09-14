using Content.Server.Radio.Components;
using Content.Shared.BF.TTS;
using Content.Shared.Chat;
using Robust.Shared.Player;

namespace Content.Server.BF.TTS.Systems;

/// <summary>
/// Uses for radio tts.
/// </summary>
public sealed class TtsHeadsetSystem : EntitySystem
{
    /// <inheritdoc cref="Robust.Shared.GameObjects.EntitySystem" />
    [Dependency] private readonly TTSSystem _ttsSystem = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<PlayRadioTtsEvent>(OnPlayRadioTts);
    }

    private async void OnPlayRadioTts(PlayRadioTtsEvent ev)
    {
        var effects = ev.Effects | TtsEffects.Radio;
        var audioData = await _ttsSystem.GenerateTTS(ev.Message, ev.Voice, effects);

        if (audioData == null)
        {
            return;
        }

        foreach (var receiver in ev.Receivers)
        {
            PlayTTSEvent playTtsEvent;
            if (TryComp<RadioSpeakerComponent>(receiver, out var speaker))
            {
                if (!speaker.Enabled)
                {
                    continue;
                }

                playTtsEvent = new PlayTTSEvent(audioData,
                    GetNetEntity(receiver),
                    true);
                RaiseNetworkEvent(playTtsEvent, Filter.Pvs(receiver, 2));

                continue;
            }

            if (!TryComp(Transform(receiver).ParentUid, out ActorComponent? actor))
            {
                continue;
            }

            playTtsEvent = new PlayTTSEvent(audioData,
                null,
                effects.HasFlag(TtsEffects.Whisper));
            RaiseNetworkEvent(playTtsEvent, actor.PlayerSession.Channel);
        }
    }

    public record PlayRadioTtsEvent(
        string Voice,
        string Message,
        TtsEffects Effects,
        List<EntityUid> Receivers);
}
