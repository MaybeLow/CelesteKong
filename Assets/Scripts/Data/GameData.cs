using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class GameData
{
    private int currentProfile;

    public List<int> UnlockedLevels = new List<int>();
    public List<int> FinishedLevels = new List<int>();
    public List<int> UnlockedAchievements = new List<int>();
    public float MusicVolume = 50.0f;
    public float SoundVolume = 50.0f;
    public Dictionary<int, int> ScoresPerLevel = new Dictionary<int, int>();
    public Dictionary<int, float> FinishTimePerLevel = new Dictionary<int, float>();

    public GameData(int profileId) 
    { 
        currentProfile = profileId;
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
