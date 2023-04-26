using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private static int levelId;

    private static bool undoActive { get; set; } = false;

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

    public static bool UndoActive()
    {
        return undoActive;
    }

    public static void EndCurrentLevel()
    {
        Debug.Log("LevelFinished");
        undoActive = false;
        AudioManager.StopBgm();
        UpdateData();
        SceneManager.LoadScene("MainMenu");
        Destroy(Instance.gameObject);
    }

    private static void UpdateData()
    {
        //DataManager.SetCurrentProfileId(0);
        DataManager.FinishedLevels.Add(levelId);
        SaveData.SaveGameData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !undoActive)
        {
            StartCoroutine(ActivateUndo());
        }
    }

    private IEnumerator ActivateUndo()
    {
        undoActive = true;
        AudioManager.ReverseAudio();
        yield return new WaitForSeconds(10.0f);
        undoActive = false;
        AudioManager.StopReverse();
    }
}
