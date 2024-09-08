using Robust.Shared.Configuration;

namespace Content.Shared.BF.CCVars;

[CVarDefs]
public sealed class BfCCVars
{
    /*
     * TTS
     */

    public static readonly CVarDef<bool> TtsEnabled =
        CVarDef.Create("tts.enabled", false, CVar.REPLICATED | CVar.ARCHIVE);

    public static readonly CVarDef<string> TtsApiUrl =
        CVarDef.Create("tts.api_url", string.Empty, CVar.SERVERONLY | CVar.ARCHIVE);

    public static readonly CVarDef<string> TtsTokenUrl =
        CVarDef.Create("tts.api_token", string.Empty, CVar.CONFIDENTIAL);

    public static readonly CVarDef<float> TtsTimeout =
        CVarDef.Create("tts.api_timeout", 2f, CVar.ARCHIVE);

    public static readonly CVarDef<float> TtsVolume =
        CVarDef.Create("tts.volume", 1f, CVar.ARCHIVE);

    public static readonly CVarDef<int> TtsMaxCache =
        CVarDef.Create("tts.max_cache", 100, CVar.ARCHIVE);

    /// <summary>
    /// Tts rate limit values are accounted in periods of this size (seconds).
    /// After the period has passed, the count resets.
    /// </summary>
    public static readonly CVarDef<int> TtsRateLimitPeriod =
        CVarDef.Create("tts.rate_limit_period", 2, CVar.ARCHIVE);

    /// <summary>
    /// How many tts preview messages are allowed in a single rate limit period.
    /// </summary>
    public static readonly CVarDef<int> TtsRateLimitCount =
        CVarDef.Create("tts.rate_limit_count", 3, CVar.ARCHIVE);
}
