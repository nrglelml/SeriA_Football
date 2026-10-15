namespace SeriA_Football.WebUI.Helpers;


/// Takım rozetleri için kısa kod (INT, NAP...) ve iki renk.
/// Veritabanında logo resmi olmadığı için rozetler bu bilgilerle çizilir.
/// Listede olmayan bir takım gelirse adın ilk 3 harfi ve nötr renkler kullanılır.

public static class TeamHelper
{
    public record TeamBadge(string Code, string Primary, string Secondary);

    private static readonly Dictionary<string, TeamBadge> Badges = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Atalanta"] = new("ATA", "#1e5aa8", "#111111"),
        ["Bologna"] = new("BOL", "#a21c26", "#1a2a57"),
        ["Cagliari"] = new("CAG", "#a3162b", "#0b2f63"),
        ["Como"] = new("COM", "#1b5fa8", "#0d2a52"),
        ["Cremonese"] = new("CRE", "#b0282d", "#6b7280"),
        ["Fiorentina"] = new("FIO", "#5b2a86", "#2e1547"),
        ["Genoa"] = new("GEN", "#b11f2e", "#0d2a52"),
        ["Hellas Verona"] = new("VER", "#0d2a6b", "#e0b100"),
        ["Inter"] = new("INT", "#0a4ea3", "#111111"),
        ["Juventus"] = new("JUV", "#1f1f1f", "#5f6368"),
        ["Lazio"] = new("LAZ", "#5aa9d6", "#1d4f75"),
        ["Lecce"] = new("LEC", "#c8102e", "#d4a900"),
        ["Milan"] = new("MIL", "#c8102e", "#111111"),
        ["Napoli"] = new("NAP", "#1d8fd8", "#0b3d6e"),
        ["Parma"] = new("PAR", "#1b3f8a", "#c9a800"),
        ["Pisa"] = new("PIS", "#0b2a5b", "#111111"),
        ["Roma"] = new("ROM", "#8e1f2f", "#c88a10"),
        ["Sassuolo"] = new("SAS", "#00843d", "#111111"),
        ["Torino"] = new("TOR", "#7a1e2c", "#3d0f16"),
        ["Udinese"] = new("UDI", "#2b2b2b", "#6b7280"),
    };

    public static TeamBadge Get(string? teamName)
    {
        if (!string.IsNullOrWhiteSpace(teamName) && Badges.TryGetValue(teamName, out var badge))
            return badge;

        var code = string.IsNullOrWhiteSpace(teamName)
            ? "?"
            : teamName.Replace(" ", "")[..Math.Min(3, teamName.Replace(" ", "").Length)].ToUpperInvariant();

        return new TeamBadge(code, "#334155", "#1e293b");
    }
}