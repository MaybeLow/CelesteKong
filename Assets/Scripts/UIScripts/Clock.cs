using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private Sprite filledClock;
    [SerializeField] private Sprite emptyClock;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update the clock sprite, depending on the time state
    private void Update()
    {
        if (GameManager.UndoAvailable() && !sr.sprite.Equals(filledClock))
        {
            sr.sprite = filledClock;
        } 
        else if (!GameManager.UndoAvailable() && !sr.sprite.Equals(emptyClock))
        {
            sr.sprite = emptyClock;
        }
    }
}
