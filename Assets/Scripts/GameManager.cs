using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private static int levelId;

    private static bool undoActive { get; set; } = false;

    private static bool undoAvailable = true;

    private static float rewindTime = 7.0f;

    // Instantiate a singleton class
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
        // Return to menu if the profile is not selected
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

    // Called, when the next level button is pressed
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

    // Exit the level safely and update data
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
        DataManager.FinishedLevels.Add(levelId);
        SaveData.SaveGameData();
    }

    // Listen to the input. When the button is pressed, reverse time for a fixed number of seconds
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

    public static void DisableUndo()
    {
        undoAvailable = false;
    }

    public static void EnableUndo()
    {
        undoAvailable = true;
    }
}
