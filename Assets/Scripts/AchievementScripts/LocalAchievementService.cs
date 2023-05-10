using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalAchievementService : MonoBehaviour, IAchievementService
{
    [SerializeField] private UI ui;
    private AudioSource unlockSound;

    private void Awake()
    {
        unlockSound = GetComponent<AudioSource>();
    }

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
            unlockSound.Play();
            ui.Notify(achievement);
            return true; 
        }
    }
}
