using System.Collections.Generic;
using System.Linq;

public static class GameSettings
{
    public static Difficulties Difficulty { get; private set; } = Difficulties.Adventure;
    public static float PlatformGenerationCooldown { get; private set; } = 4.5f;
    public static float PlatformGenerationSpeedFactor { get; private set; } = 1f;
    public static float PotionGenerationCooldown { get; private set; } = 30f;
    public static Sceneries Scenery { get; private set; } = Sceneries.ConstructionSite;
    public static List<Scenery> UnlockedSceneries => AvailableSceneries.Where(s => s.IsUnlocked).ToList();

    public static readonly List<Scenery> AvailableSceneries = new List<Scenery>()
    {
        new Scenery(Sceneries.ConstructionSite, true),
        new Scenery(Sceneries.Forest),
        new Scenery(Sceneries.City),
    };

    public static void Init(Sceneries scenery, Difficulties difficulty)
    {
        Scenery = scenery;
        Difficulty = difficulty;

        switch (Difficulty)
        {
            case Difficulties.Adventure:
                PlatformGenerationSpeedFactor = 1f;
                PotionGenerationCooldown = 30f;
                break;
            case Difficulties.Challenge:
                PlatformGenerationSpeedFactor = 2f;
                PotionGenerationCooldown = 60f;
                break;
            case Difficulties.Nightmare:
                PlatformGenerationSpeedFactor = 4f;
                PotionGenerationCooldown = 120f;
                break;
        }
    }

    public static void UpdateSceneryHighestScore(int score)
    {
        var currentScenery = AvailableSceneries.Where(s => s.Type == Scenery).FirstOrDefault();

        if (currentScenery != null && currentScenery.HighestScore < score)
        {
            currentScenery.HighestScore = score;
        }
    }

    public static void UpdateUnlockedSceneries(List<int> unlockedSceneries)
    {
        foreach (var scenery in AvailableSceneries)
        {
            if (unlockedSceneries.Contains((int)scenery.Type))
            {
                scenery.IsUnlocked = true;
            }
        }
    }
}

public enum Difficulties
{
    Adventure,
    Challenge,
    Nightmare,
}
