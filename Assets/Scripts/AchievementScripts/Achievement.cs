using UnityEngine;

public abstract class Achievement : MonoBehaviour
{
    protected LocalAchievementService achievementService;

    [SerializeField] private int achievementID;
    [SerializeField] private string achievementDescription;

    private void Awake()
    {
        achievementService = transform.parent.GetComponent<LocalAchievementService>();
    }

    public int GetID()
    {
        return achievementID;
    }

    public string GetDescription()
    {
        return achievementDescription;
    }
}
