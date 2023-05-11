
public class TimeReverseAchievement : Achievement
{
    private bool timeWasReversed = false;

    // Unlock if time was not reversed
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
