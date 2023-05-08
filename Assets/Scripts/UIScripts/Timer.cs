using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    TMP_Text text;
    // Start is called before the first frame update
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        text.text = Time.timeSinceLevelLoad.ToString("F2");
    }
}
