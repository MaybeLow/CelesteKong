using UnityEngine;

public class TimeReverseManager : MonoBehaviour
{
    // Disable the ability to reverse time
    void Start()
    {
        GameManager.DisableUndo();
    }
}
