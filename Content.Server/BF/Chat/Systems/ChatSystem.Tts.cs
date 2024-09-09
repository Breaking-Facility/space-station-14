using System.Threading.Tasks;
using Content.Server.BF.TTS;
using Content.Shared.BF.TTS;
using Content.Shared.Chat;
using Robust.Shared.Player;

namespace Content.Server.Chat.Systems;

// TODO refactor whatever active warzone this class and chatmanager have become
/// <summary>
/// </summary>
public sealed partial class ChatSystem : SharedChatSystem
{
    [Dependency] private readonly TTSSystem _ttsSystem = default!;

    private async Task SendTts(string text, Filter filter)
    {
        var audioData = await _ttsSystem.GenerateTTS(text, "Gladas", TtsEffects.Announce);
        if (audioData == null)
        {
            return;
        }

        var ev = new PlayTTSEvent(audioData);
        RaiseNetworkEvent(ev, filter);
    }
}
