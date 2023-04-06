using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private Transform tr;
    [SerializeField] private MenuCamera mc;
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject levelSelectionCanvas;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject profileCanvas;
    [SerializeField] private GameObject achievementCanvas;
    private GameObject currentCanvas;

    private void Awake()
    {
        tr = GetComponent<Transform>();
    }

    private void Start()
    {
        levelSelectionCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        profileCanvas.SetActive(false);
        achievementCanvas.SetActive(false);

        currentCanvas = mainCanvas;
        mainCanvas.SetActive(true);

        mc.UpdateCentre(new Vector2(0, 0));
    }

    public void OnPlayButton()
    {
        Debug.Log("Play button is pressed");
        mc.UpdateCentre(new Vector2(-20f, 0));
        changeCurrentCanvas(levelSelectionCanvas);
    }

    public void OnBackToMainButton()
    {
        Debug.Log("Back button is pressed");
        mc.UpdateCentre(new Vector2(0, 0));
        changeCurrentCanvas(mainCanvas);
    }

    public void OnLevelSelectionButton(int levelId)
    {
        Debug.Log("Level: " + levelId);
        //mc.UpdateCentre(new Vector2(-20f, 0));
        //canvas.SetActive(false);
    }

    public void OnProfileSelectionButton(int profileId)
    {
        Debug.Log("Profile: " + profileId);
        //mc.UpdateCentre(new Vector2(-20f, 0));
        //canvas.SetActive(false);
    }

    public void OnSettingsButton()
    {
        Debug.Log("Settings button is pressed");
        mc.UpdateCentre(new Vector2(20f, 0));
        changeCurrentCanvas(settingsCanvas);
    }

    public void OnProfileButton()
    {
        Debug.Log("Profile button is pressed");
        mc.UpdateCentre(new Vector2(0, -20f));
        changeCurrentCanvas(profileCanvas);
    }

    public void OnAchievementButton()
    {
        Debug.Log("Achievement button is pressed");
        mc.UpdateCentre(new Vector2(0, 20f));
        changeCurrentCanvas(achievementCanvas);
    }

    public void OnQuitButton()
    {
        Debug.Log("Quit button is pressed");
    }

    private void changeCurrentCanvas(GameObject newCanvas)
    {
        currentCanvas.SetActive(false);
        currentCanvas = newCanvas;
        currentCanvas.SetActive(true);
    }
}
