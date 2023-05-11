using System.Collections;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject achievementNotif;

    public void Notify(Achievement achievement)
    {
        StartCoroutine(NotificationCoroutine(achievement));
    }

    // Display an achievement notification for a fixed amount of time
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
