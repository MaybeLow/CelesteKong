using UnityEngine;

public class TimeReverseAchievement : Achievement
{
    private bool timeWasReversed = false;

    public void TimeReverse(bool reversing)
    {
        if (reversing)
        {
            timeWasReversed = true;
        } 
        else if (!timeWasReversed)
        {
            achievementService.UnlockAchievement(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !GameManager.UndoActive() && !GameManager.UndoAvailable())
        {
            timeWasReversed = true;
        }
    }
}
