using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private Transform tr;
    [SerializeField] private MenuCamera mc;
    [SerializeField] private GameObject canvas;

    private void Awake()
    {
        tr = GetComponent<Transform>();
    }

    public void OnPlayButton()
    {
        Debug.Log("Play button is pressed");
        mc.UpdateCentre(new Vector2(-20f, 0));
        canvas.SetActive(false);
    }

    public void OnQuitButton()
    {
        Debug.Log("Quit button is pressed");
    }
}
