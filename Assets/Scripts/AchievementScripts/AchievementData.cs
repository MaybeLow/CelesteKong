using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementData : Achievement
{
    private int jumpsToUnlock = 10;
    private int numOfJumps = 0;

    public void Jump()
    {
        if (!achievementService.IsAchievementUnlocked(this))
        {
            Debug.Log(numOfJumps);
            numOfJumps++;
            if (numOfJumps > jumpsToUnlock)
            {
                achievementService.UnlockAchievement(this);
            }
        }
        else
        {
            Debug.Log("Achievement Unlocked!");
        }
    }
}
