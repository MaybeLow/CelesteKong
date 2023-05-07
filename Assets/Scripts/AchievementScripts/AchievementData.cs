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
            numOfJumps++;
        }
    }

    public void FinishLevelJump()
    {
        if (!achievementService.IsAchievementUnlocked(this))
        {
            if (numOfJumps < jumpsToUnlock)
            {
                achievementService.UnlockAchievement(this);
            }
        }
    }
}
