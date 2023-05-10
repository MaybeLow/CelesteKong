using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private static int levelId;

    private static bool undoActive { get; set; } = false;

    private static bool undoAvailable = true;

    private static float rewindTime = 7.0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        levelId = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        if (DataManager.GetCurrentProfileId() == -1)
        {
            AudioManager.StopBgm();
            SceneManager.LoadScene("MainMenu");
            Destroy(Instance.gameObject);
        }
    }

    public static float GetRewindTime() { 
        return rewindTime; 
    }

    public static bool UndoActive()
    {
        return undoActive;
    }

    public static bool UndoAvailable()
    {
        return undoAvailable;
    }

    public static void FinishCurrentLevel()
    {
        DataManager.FinishedLevels.Add(levelId);
        if (levelId + 1 < SceneManager.sceneCountInBuildSettings) {
            DataManager.UnlockedLevels.Add(levelId + 1);
            SceneManager.LoadScene(levelId + 1);
        } 
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
        EndCurrentLevel();
    }

    private static void EndCurrentLevel()
    {
        undoActive = false;
        undoAvailable = true;
        AudioManager.StopBgm();
        UpdateData();
        Destroy(Instance.gameObject);
    }

    public static void OnExitButton()
    {
        SceneManager.LoadScene("MainMenu");
        EndCurrentLevel();
    }

    public static void OnPlayerDead()
    {
        undoActive = false;
        undoAvailable = true;
    }

    private static void UpdateData()
    {
        //DataManager.SetCurrentProfileId(0);
        DataManager.FinishedLevels.Add(levelId);
        SaveData.SaveGameData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !undoActive && undoAvailable)
        {
            StartCoroutine(ActivateUndo());
        }
    }

    private IEnumerator ActivateUndo()
    {
        undoAvailable = false;
        undoActive = true;
        AudioManager.ReverseAudio();
        yield return new WaitForSeconds(rewindTime);
        undoActive = false;
        AudioManager.StopReverse();
        yield return new WaitForSeconds(rewindTime);
        undoAvailable = true;
    }
}
