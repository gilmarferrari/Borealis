using System;

public class Scenery
{
    public Sceneries Type { get; set; }
    public string Description { get; set; }
    public bool IsUnlocked { get; set; }
    public int HighestScore { get; set; }

    public Scenery(Sceneries type, bool unlocked = false)
    {
        Type = type;
        Description = $"{type}";
        IsUnlocked = unlocked;
        HighestScore = 0;
    }
}

public enum Sceneries
{
    ConstructionSite = 0,
    Forest = 1,
    City = 2,
}
