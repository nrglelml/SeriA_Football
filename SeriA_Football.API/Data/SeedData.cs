using SeriA_Football.API.Entities;
using SeriA_Football.API.Entities.Enums;

namespace SerieA.API.Data;

/// <summary>
/// Demo veri: 20 takım, 3 hafta (30 maç), 4 detaylı maç.
/// Hafta 1 ve 2 tamamen oynandı; Hafta 3'te 6 maç oynandı, 4 maç henüz oynanmadı.
/// Not: Detaylı OLMAYAN maçlarda gol/kart kaydı yoktur, skor sadece Match tablosundadır.
/// </summary>
public static class SeedData
{
    // ---------------------------------------------------------------
    // TAKIMLAR (Id'ler sabit, aşağıdaki maçlar bu Id'lere göre yazıldı)
    // ---------------------------------------------------------------
    public static readonly Team[] Teams =
    {
        new() { Id = 1,  Name = "Atalanta",      City = "Bergamo",  Stadium = "Gewiss Stadium",                    LogoUrl = "/images/logos/atalanta.png" },
        new() { Id = 2,  Name = "Bologna",       City = "Bologna",  Stadium = "Stadio Renato Dall'Ara",            LogoUrl = "/images/logos/bologna.png" },
        new() { Id = 3,  Name = "Cagliari",      City = "Cagliari", Stadium = "Unipol Domus",                      LogoUrl = "/images/logos/cagliari.png" },
        new() { Id = 4,  Name = "Como",          City = "Como",     Stadium = "Stadio Giuseppe Sinigaglia",        LogoUrl = "/images/logos/como.png" },
        new() { Id = 5,  Name = "Cremonese",     City = "Cremona",  Stadium = "Stadio Giovanni Zini",              LogoUrl = "/images/logos/cremonese.png" },
        new() { Id = 6,  Name = "Fiorentina",    City = "Floransa", Stadium = "Stadio Artemio Franchi",            LogoUrl = "/images/logos/fiorentina.png" },
        new() { Id = 7,  Name = "Genoa",         City = "Cenova",   Stadium = "Stadio Luigi Ferraris",             LogoUrl = "/images/logos/genoa.png" },
        new() { Id = 8,  Name = "Hellas Verona", City = "Verona",   Stadium = "Stadio Marcantonio Bentegodi",      LogoUrl = "/images/logos/verona.png" },
        new() { Id = 9,  Name = "Inter",         City = "Milano",   Stadium = "San Siro",                          LogoUrl = "/images/logos/inter.png" },
        new() { Id = 10, Name = "Juventus",      City = "Torino",   Stadium = "Allianz Stadium",                   LogoUrl = "/images/logos/juventus.png" },
        new() { Id = 11, Name = "Lazio",         City = "Roma",     Stadium = "Stadio Olimpico",                   LogoUrl = "/images/logos/lazio.png" },
        new() { Id = 12, Name = "Lecce",         City = "Lecce",    Stadium = "Stadio Via del Mare",               LogoUrl = "/images/logos/lecce.png" },
        new() { Id = 13, Name = "Milan",         City = "Milano",   Stadium = "San Siro",                          LogoUrl = "/images/logos/milan.png" },
        new() { Id = 14, Name = "Napoli",        City = "Napoli",   Stadium = "Stadio Diego Armando Maradona",     LogoUrl = "/images/logos/napoli.png" },
        new() { Id = 15, Name = "Parma",         City = "Parma",    Stadium = "Stadio Ennio Tardini",              LogoUrl = "/images/logos/parma.png" },
        new() { Id = 16, Name = "Pisa",          City = "Pisa",     Stadium = "Arena Garibaldi",                   LogoUrl = "/images/logos/pisa.png" },
        new() { Id = 17, Name = "Roma",          City = "Roma",     Stadium = "Stadio Olimpico",                   LogoUrl = "/images/logos/roma.png" },
        new() { Id = 18, Name = "Sassuolo",      City = "Sassuolo", Stadium = "Mapei Stadium",                     LogoUrl = "/images/logos/sassuolo.png" },
        new() { Id = 19, Name = "Torino",        City = "Torino",   Stadium = "Stadio Olimpico Grande Torino",     LogoUrl = "/images/logos/torino.png" },
        new() { Id = 20, Name = "Udinese",       City = "Udine",    Stadium = "Bluenergy Stadium",                 LogoUrl = "/images/logos/udinese.png" },
    };

    private static string StadiumOf(int teamId) => Teams.First(t => t.Id == teamId).Stadium;
    private static DateTime D(int month, int day, int hour, int minute) => new(2026, month, day, hour, minute, 0);

    private static Match Played(int id, int week, int home, int away, DateTime date, int homeScore, int awayScore) => new()
    {
        Id = id,
        Week = week,
        HomeTeamId = home,
        AwayTeamId = away,
        MatchDate = date,
        HomeScore = homeScore,
        AwayScore = awayScore,
        Status = MatchStatus.Completed,
        Stadium = StadiumOf(home)
    };

    private static Match Upcoming(int id, int week, int home, int away, DateTime date) => new()
    {
        Id = id,
        Week = week,
        HomeTeamId = home,
        AwayTeamId = away,
        MatchDate = date,
        HomeScore = null,
        AwayScore = null,
        Status = MatchStatus.Scheduled,
        Stadium = StadiumOf(home)
    };

    // ---------------------------------------------------------------
    // MAÇLAR  (ev sahibi - deplasman)
    // Kural kontrolü: her hafta 20 takımın hepsi tam 1 kez oynuyor.
    // Aynı stadı paylaşanlar (Inter/Milan, Roma/Lazio) aynı hafta ikisi birden ev sahibi değil.
    // ---------------------------------------------------------------
    public static readonly Match[] Matches =
    {
        // ===== 1. HAFTA (22-24 Ağustos 2026) =====
        Played(1,  1,  9, 19, D(8, 22, 20, 45), 3, 1), // Inter - Torino          (DETAYLI)
        Played(2,  1, 10, 15, D(8, 23, 20, 45), 2, 0), // Juventus - Parma
        Played(3,  1, 14, 18, D(8, 22, 18, 30), 2, 1), // Napoli - Sassuolo
        Played(4,  1, 17,  2, D(8, 23, 18, 0),  1, 1), // Roma - Bologna
        Played(5,  1,  1, 16, D(8, 23, 15, 0),  4, 0), // Atalanta - Pisa
        Played(6,  1,  6,  3, D(8, 23, 15, 0),  1, 0), // Fiorentina - Cagliari
        Played(7,  1,  5, 13, D(8, 22, 15, 0),  1, 2), // Cremonese - Milan
        Played(8,  1,  8, 11, D(8, 24, 20, 45), 0, 0), // Hellas Verona - Lazio
        Played(9,  1, 12,  7, D(8, 24, 18, 30), 2, 2), // Lecce - Genoa
        Played(10, 1, 20,  4, D(8, 23, 12, 30), 0, 1), // Udinese - Como

        // ===== 2. HAFTA (29-31 Ağustos 2026) =====
        Played(11, 2, 13, 20, D(8, 29, 20, 45), 2, 0), // Milan - Udinese
        Played(12, 2, 11, 14, D(8, 30, 20, 45), 1, 3), // Lazio - Napoli          (DETAYLI)
        Played(13, 2, 19,  1, D(8, 30, 18, 0),  1, 1), // Torino - Atalanta
        Played(14, 2, 16, 17, D(8, 29, 18, 30), 0, 2), // Pisa - Roma
        Played(15, 2,  7, 10, D(8, 30, 15, 0),  1, 1), // Genoa - Juventus
        Played(16, 2,  4,  9, D(8, 29, 15, 0),  0, 2), // Como - Inter
        Played(17, 2,  2,  6, D(8, 31, 20, 45), 2, 1), // Bologna - Fiorentina
        Played(18, 2,  3,  8, D(8, 30, 12, 30), 2, 0), // Cagliari - Hellas Verona
        Played(19, 2, 18,  5, D(8, 31, 18, 30), 3, 2), // Sassuolo - Cremonese    (DETAYLI)
        Played(20, 2, 15, 12, D(8, 30, 15, 0),  1, 0), // Parma - Lecce

        // ===== 3. HAFTA (12-14 Eylül 2026) - 6 maç oynandı, 4 maç planlı =====
        Played(21, 3,  9, 11, D(9, 12, 20, 45), 2, 1), // Inter - Lazio
        Played(22, 3, 17, 13, D(9, 13, 20, 45), 1, 1), // Roma - Milan            (DETAYLI)
        Played(23, 3, 10,  1, D(9, 12, 18, 0),  0, 1), // Juventus - Atalanta
        Played(24, 3, 14,  7, D(9, 13, 15, 0),  3, 0), // Napoli - Genoa
        Played(25, 3,  6, 19, D(9, 12, 15, 0),  2, 2), // Fiorentina - Torino
        Played(26, 3,  2,  3, D(9, 13, 18, 0),  1, 0), // Bologna - Cagliari
        Upcoming(27, 3, 20, 15, D(9, 14, 16, 30)),      // Udinese - Parma
        Upcoming(28, 3,  8, 18, D(9, 14, 20, 45)),      // Hellas Verona - Sassuolo
        Upcoming(29, 3, 12, 16, D(9, 14, 18, 30)),      // Lecce - Pisa
        Upcoming(30, 3,  5,  4, D(9, 14, 20, 45)),      // Cremonese - Como
    };

    // ---------------------------------------------------------------
    // GOLLER  (gol sayıları ilgili maçın skoruyla birebir tutarlı)
    // ---------------------------------------------------------------
    public static readonly MatchGoal[] Goals =
    {
        // Maç 1: Inter 3-1 Torino
        new() { Id = 1,  MatchId = 1,  TeamId = 9,  PlayerName = "Lautaro Martínez",  Minute = 12 },
        new() { Id = 2,  MatchId = 1,  TeamId = 19, PlayerName = "Giovanni Simeone",  Minute = 34 },
        new() { Id = 3,  MatchId = 1,  TeamId = 9,  PlayerName = "Marcus Thuram",     Minute = 58 },
        new() { Id = 4,  MatchId = 1,  TeamId = 9,  PlayerName = "Nicolò Barella",    Minute = 81 },

        // Maç 12: Lazio 1-3 Napoli
        new() { Id = 5,  MatchId = 12, TeamId = 14, PlayerName = "Scott McTominay",   Minute = 21 },
        new() { Id = 6,  MatchId = 12, TeamId = 11, PlayerName = "Mattia Zaccagni",   Minute = 39 },
        new() { Id = 7,  MatchId = 12, TeamId = 14, PlayerName = "Rasmus Højlund",    Minute = 55 },
        new() { Id = 8,  MatchId = 12, TeamId = 14, PlayerName = "Matteo Politano",   Minute = 87 },

        // Maç 19: Sassuolo 3-2 Cremonese
        new() { Id = 9,  MatchId = 19, TeamId = 18, PlayerName = "Domenico Berardi",  Minute = 8 },
        new() { Id = 10, MatchId = 19, TeamId = 5,  PlayerName = "Jamie Vardy",       Minute = 25 },
        new() { Id = 11, MatchId = 19, TeamId = 18, PlayerName = "Andrea Pinamonti",  Minute = 47 },
        new() { Id = 12, MatchId = 19, TeamId = 5,  PlayerName = "Federico Bonazzoli",Minute = 63 },
        new() { Id = 13, MatchId = 19, TeamId = 18, PlayerName = "Armand Laurienté",  Minute = 85 },

        // Maç 22: Roma 1-1 Milan
        new() { Id = 14, MatchId = 22, TeamId = 17, PlayerName = "Paulo Dybala",      Minute = 38 },
        new() { Id = 15, MatchId = 22, TeamId = 13, PlayerName = "Rafael Leão",       Minute = 72 },
    };

    // ---------------------------------------------------------------
    // KARTLAR
    // ---------------------------------------------------------------
    public static readonly MatchCard[] Cards =
    {
        // Maç 1: Inter - Torino
        new() { Id = 1,  MatchId = 1,  TeamId = 19, PlayerName = "Saúl Coco",          Minute = 27, CardType = CardType.Yellow },
        new() { Id = 2,  MatchId = 1,  TeamId = 9,  PlayerName = "Hakan Çalhanoğlu",   Minute = 44, CardType = CardType.Yellow },
        new() { Id = 3,  MatchId = 1,  TeamId = 19, PlayerName = "Kristjan Asllani",   Minute = 69, CardType = CardType.Yellow },

        // Maç 12: Lazio - Napoli  (Romagnoli 2. sarıdan atıldı, Lazio 10 kişi kaldı)
        new() { Id = 4,  MatchId = 12, TeamId = 11, PlayerName = "Matteo Guendouzi",   Minute = 30, CardType = CardType.Yellow },
        new() { Id = 5,  MatchId = 12, TeamId = 14, PlayerName = "Stanislav Lobotka",  Minute = 52, CardType = CardType.Yellow },
        new() { Id = 6,  MatchId = 12, TeamId = 11, PlayerName = "Alessio Romagnoli",  Minute = 74, CardType = CardType.Yellow },
        new() { Id = 7,  MatchId = 12, TeamId = 11, PlayerName = "Alessio Romagnoli",  Minute = 79, CardType = CardType.SecondYellow },

        // Maç 19: Sassuolo - Cremonese
        new() { Id = 8,  MatchId = 19, TeamId = 18, PlayerName = "Nemanja Matić",      Minute = 19, CardType = CardType.Yellow },
        new() { Id = 9,  MatchId = 19, TeamId = 5,  PlayerName = "Warren Bondo",       Minute = 56, CardType = CardType.Yellow },
        new() { Id = 10, MatchId = 19, TeamId = 5,  PlayerName = "Franco Vázquez",     Minute = 90, CardType = CardType.Yellow },

        // Maç 22: Roma - Milan  (Tomori direkt kırmızı)
        new() { Id = 11, MatchId = 22, TeamId = 17, PlayerName = "Gianluca Mancini",   Minute = 15, CardType = CardType.Yellow },
        new() { Id = 12, MatchId = 22, TeamId = 13, PlayerName = "Youssouf Fofana",    Minute = 50, CardType = CardType.Yellow },
        new() { Id = 13, MatchId = 22, TeamId = 17, PlayerName = "Bryan Cristante",    Minute = 61, CardType = CardType.Yellow },
        new() { Id = 14, MatchId = 22, TeamId = 13, PlayerName = "Fikayo Tomori",      Minute = 89, CardType = CardType.Red },
    };

    // ---------------------------------------------------------------
    // OYUNCU DEĞİŞİKLİKLERİ  (PlayerIn = giren, PlayerOut = çıkan)
    // ---------------------------------------------------------------
    public static readonly Substitution[] Substitutions =
    {
        // Maç 1: Inter - Torino
        new() { Id = 1,  MatchId = 1,  TeamId = 19, PlayerIn = "Duván Zapata",        PlayerOut = "Giovanni Simeone",   Minute = 60 },
        new() { Id = 2,  MatchId = 1,  TeamId = 19, PlayerIn = "Cyril Ngonge",        PlayerOut = "Nikola Vlašić",      Minute = 66 },
        new() { Id = 3,  MatchId = 1,  TeamId = 9,  PlayerIn = "Ange-Yoan Bonny",     PlayerOut = "Marcus Thuram",      Minute = 70 },
        new() { Id = 4,  MatchId = 1,  TeamId = 9,  PlayerIn = "Davide Frattesi",     PlayerOut = "Hakan Çalhanoğlu",   Minute = 75 },
        new() { Id = 5,  MatchId = 1,  TeamId = 9,  PlayerIn = "Piotr Zieliński",     PlayerOut = "Nicolò Barella",     Minute = 84 },

        // Maç 12: Lazio - Napoli
        new() { Id = 6,  MatchId = 12, TeamId = 11, PlayerIn = "Boulaye Dia",         PlayerOut = "Valentín Castellanos", Minute = 61 },
        new() { Id = 7,  MatchId = 12, TeamId = 14, PlayerIn = "David Neres",         PlayerOut = "Kevin De Bruyne",    Minute = 66 },
        new() { Id = 8,  MatchId = 12, TeamId = 11, PlayerIn = "Pedro",               PlayerOut = "Gustav Isaksen",     Minute = 70 },
        new() { Id = 9,  MatchId = 12, TeamId = 14, PlayerIn = "Billy Gilmour",       PlayerOut = "Stanislav Lobotka",  Minute = 80 },

        // Maç 19: Sassuolo - Cremonese
        new() { Id = 10, MatchId = 19, TeamId = 18, PlayerIn = "Cristian Volpato",    PlayerOut = "Domenico Berardi",   Minute = 68 },
        new() { Id = 11, MatchId = 19, TeamId = 5,  PlayerIn = "Antonio Sanabria",    PlayerOut = "Jamie Vardy",        Minute = 70 },
        new() { Id = 12, MatchId = 19, TeamId = 5,  PlayerIn = "Alberto Grassi",      PlayerOut = "Warren Bondo",       Minute = 76 },
        new() { Id = 13, MatchId = 19, TeamId = 18, PlayerIn = "Luca Lipani",         PlayerOut = "Nemanja Matić",      Minute = 82 },

        // Maç 22: Roma - Milan
        new() { Id = 14, MatchId = 22, TeamId = 13, PlayerIn = "Christopher Nkunku",  PlayerOut = "Santiago Giménez",   Minute = 60 },
        new() { Id = 15, MatchId = 22, TeamId = 17, PlayerIn = "Evan Ferguson",       PlayerOut = "Artem Dovbyk",       Minute = 65 },
        new() { Id = 16, MatchId = 22, TeamId = 13, PlayerIn = "Ruben Loftus-Cheek",  PlayerOut = "Luka Modrić",        Minute = 70 },
        new() { Id = 17, MatchId = 22, TeamId = 17, PlayerIn = "Stephan El Shaarawy", PlayerOut = "Paulo Dybala",       Minute = 75 },
    };
}