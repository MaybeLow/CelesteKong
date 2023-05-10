using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject achievementNotif;

    public void Notify(Achievement achievement)
    {
        StartCoroutine(NotificationCoroutine(achievement));
    }

    private IEnumerator NotificationCoroutine(Achievement achievement)
    {
        GameObject n = Instantiate(achievementNotif, transform);
        n.transform.localPosition = new Vector2(0.0f, 160.0f);

        TMP_Text text = n.GetComponentInChildren<TMP_Text>();
        text.text = achievement.GetDescription();

        yield return new WaitForSeconds(2.0f);
        Destroy(n);
    }
}
