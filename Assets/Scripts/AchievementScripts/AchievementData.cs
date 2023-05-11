
public class AchievementData : Achievement
{
    private int jumpsToUnlock = 20;
    private int numOfJumps = 0;

    // Add a jump to the jump counter
    public void Jump()
    {
        if (!achievementService.IsAchievementUnlocked(this))
        {
            numOfJumps++;
        }
    }

    // Unlock achievement only if the player did not exceet the limit of jumps
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
