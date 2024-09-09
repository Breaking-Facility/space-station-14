using Content.Server.Chat.Managers;
using Content.Server.Players.RateLimiting;
using Content.Shared.BF.CCVars;
using Robust.Shared.Player;

namespace Content.Server.BF.TTS;

public sealed partial class TTSSystem
{
    [Dependency] private readonly PlayerRateLimitManager _rateLimitManager = default!;
    [Dependency] private readonly IChatManager _chat = default!;

    private const string RateLimitKey = "TTS";

    private void RegisterRateLimits()
    {
        _rateLimitManager.Register(RateLimitKey,
            new RateLimitRegistration
            {
                CVarLimitPeriodLength = BfCCVars.TtsRateLimitPeriod,
                CVarLimitCount = BfCCVars.TtsRateLimitCount,
                PlayerLimitedAction = RateLimitPlayerLimited,
            });
    }

    private void RateLimitPlayerLimited(ICommonSession player)
    {
        _chat.DispatchServerMessage(player, Loc.GetString("tts-rate-limited"), suppressLog: true);
    }

    private RateLimitStatus HandleRateLimit(ICommonSession player)
    {
        return _rateLimitManager.CountAction(player, RateLimitKey);
    }
}
