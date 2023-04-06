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
        mainCanvas.SetActive(false);

        mc.UpdateCentre(new Vector2(0, -20f));
        currentCanvas = profileCanvas;
        currentCanvas.SetActive(true);
    }

    public void OnPlayButton()
    {
        Debug.Log("Play button is pressed");
        mc.UpdateCentre(new Vector2(-20f, 0));
        ChangeCurrentCanvas(levelSelectionCanvas);
    }

    public void OnBackToMainButton()
    {
        Debug.Log("Back button is pressed");
        mc.UpdateCentre(new Vector2(0, 0));
        ChangeCurrentCanvas(mainCanvas);
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
        
        SaveData.LoadGameData(profileId);

        mc.UpdateCentre(new Vector2(0, 0));
        ChangeCurrentCanvas(mainCanvas);
    }

    public void OnSettingsButton()
    {
        Debug.Log("Settings button is pressed");
        mc.UpdateCentre(new Vector2(20f, 0));
        ChangeCurrentCanvas(settingsCanvas);
    }

    public void OnProfileButton()
    {
        Debug.Log("Profile button is pressed");
        mc.UpdateCentre(new Vector2(0, -20f));
        ChangeCurrentCanvas(profileCanvas);
    }

    public void OnAchievementButton()
    {
        Debug.Log("Achievement button is pressed");
        mc.UpdateCentre(new Vector2(0, 20f));
        ChangeCurrentCanvas(achievementCanvas);
    }

    public void OnQuitButton()
    {
        Debug.Log("Quit button is pressed");
    }

    private void ChangeCurrentCanvas(GameObject newCanvas)
    {
        currentCanvas.SetActive(false);
        currentCanvas = newCanvas;
        currentCanvas.SetActive(true);
    }

    public void TestSaves()
    {
        Debug.Log("Volume pre: " + DataManager.MusicVolume);
        DataManager.MusicVolume += 5.0f;
        Debug.Log("Volume pre: " + DataManager.MusicVolume);
    }

    public void TestSaves2()
    {
        SaveData.SaveGameData();
    }
}
