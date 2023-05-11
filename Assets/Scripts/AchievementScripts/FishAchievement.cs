using UnityEngine;

public class FishAchievement : Achievement
{
    // achievement unlocked when the fish is collected by the player
    public void CollectFish()
    {
        if (!achievementService.IsAchievementUnlocked(this))
        {
            achievementService.UnlockAchievement(this);
            Debug.Log("Fish Collected!");
        }
        else
        {
            Debug.Log("Achievement already unlocked!");
        }
    }
}
