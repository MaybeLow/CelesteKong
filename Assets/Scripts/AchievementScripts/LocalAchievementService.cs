using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalAchievementService : MonoBehaviour, IAchievementService
{
    public bool IsAchievementUnlocked(Achievement achievement)
    {
        return DataManager.UnlockedAchievements.Contains(achievement.GetID());
    }

    public bool UnlockAchievement(Achievement achievement)
    {
        if (IsAchievementUnlocked(achievement))
        {
            return false;
        }
        else 
        {
            DataManager.UnlockedAchievements.Add(achievement.GetID());
            SaveData.SaveGameData();
            return true; 
        }
    }
}
