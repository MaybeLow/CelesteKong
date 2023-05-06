using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[System.Serializable]
public class GameData
{
    private int currentProfile;

    public HashSet<int> UnlockedLevels = new HashSet<int>();
    public HashSet<int> FinishedLevels = new HashSet<int>();
    public HashSet<int> UnlockedAchievements = new HashSet<int>();
    public float MusicVolume = 50.0f;
    public float SoundVolume = 50.0f;
    public Dictionary<int, int> ScoresPerLevel = new Dictionary<int, int>();
    public Dictionary<int, float> FinishTimePerLevel = new Dictionary<int, float>();

    public GameData(int profileId) 
    { 
        currentProfile = profileId;
        UnlockedLevels.Add(1);
    }

    public int GetProfileId()
    {
        return currentProfile;
    }

    public void FillGameData()
    {
        currentProfile = DataManager.GetCurrentProfileId();
        UnlockedLevels = DataManager.UnlockedLevels;
        FinishedLevels = DataManager.FinishedLevels;
        UnlockedAchievements = DataManager.UnlockedAchievements;
        MusicVolume = DataManager.MusicVolume;
        SoundVolume = DataManager.SoundVolume;
        ScoresPerLevel = DataManager.ScoresPerLevel;
        FinishTimePerLevel = DataManager.FinishTimePerLevel;
    }
}
