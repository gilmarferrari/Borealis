using System;
using System.Collections.Generic;
using System.Linq;


[Serializable]
public class PlayerData
{
    public List<int> UnlockedSceneries;
    public Dictionary<int, int> SceneriesHighestScores;

    public PlayerData()
    {
        // Player Data
        UnlockedSceneries = GameSettings.UnlockedSceneries.Select(s => (int)s.Type).ToList();
        SceneriesHighestScores = GameSettings.UnlockedSceneries.ToDictionary(s => (int)s.Type, s => s.HighestScore);
    }
}
