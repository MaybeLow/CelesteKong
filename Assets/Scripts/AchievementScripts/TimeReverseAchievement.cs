using UnityEngine;

public class TimeReverseAchievement : Achievement
{
    private bool timeWasReversed = false;

    public void TimeReverse()
    {
        if (!timeWasReversed)
        {
            achievementService.UnlockAchievement(this);
        }
    }

    public void ReverseCheck()
    {
        timeWasReversed = true;
    }
}
