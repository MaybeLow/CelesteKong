using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    private static int currentProfile;

    public static HashSet<int> UnlockedLevels;
    public static HashSet<int> FinishedLevels;
    public static HashSet<int> UnlockedAchievements;
    public static float MusicVolume;
    public static float SoundVolume;
    public static Dictionary<int, int> ScoresPerLevel;
    public static Dictionary<int, float> FinishTimePerLevel;

    public static int GetCurrentProfileId()
    {
        return currentProfile;
    }

    public static void FillData(GameData data)
    {
        currentProfile = data.GetProfileId();
        UnlockedLevels = data.UnlockedLevels;
        FinishedLevels = data.FinishedLevels;
        UnlockedAchievements = data.UnlockedAchievements;
        MusicVolume = data.MusicVolume;
        SoundVolume = data.SoundVolume;
        ScoresPerLevel = data.ScoresPerLevel;
        FinishTimePerLevel = data.FinishTimePerLevel;
    }
}
