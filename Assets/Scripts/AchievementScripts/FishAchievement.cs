using UnityEngine;

public class FishAchievement : Achievement
{
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
