using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update the timer value
    private void FixedUpdate()
    {
        text.text = Time.timeSinceLevelLoad.ToString("F2");
    }
}
