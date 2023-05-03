using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private LocalAchievementService achievementService;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player_"))
        {
            gameObject.SetActive(false);
            achievementService.BroadcastMessage("CollectFish");
        }
    }
}
