using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAchievementService
{
    public bool IsAchievementUnlocked(Achievement achievement);

    public bool UnlockAchievement(Achievement achievement);
}