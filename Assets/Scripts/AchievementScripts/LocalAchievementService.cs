using UnityEngine;

public class LocalAchievementService : MonoBehaviour, IAchievementService
{
    [SerializeField] private UI ui;
    private AudioSource unlockSound;

    private void Awake()
    {
        unlockSound = GetComponent<AudioSource>();
    }

    // Check if the achievement is already unlocked
    public bool IsAchievementUnlocked(Achievement achievement)
    {
        return DataManager.UnlockedAchievements.Contains(achievement.GetID());
    }

    // Unlock an achievement and display a notification
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
