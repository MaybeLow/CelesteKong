using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private static int levelId;

    private static bool undoActive { get; set; } = false;

    public void Awake()
    {
        Instance = this;
        levelId = SceneManager.GetActiveScene().buildIndex;
    }

    public static bool UndoActive()
    {
        return undoActive;
    }

    public static void EndCurrentLevel()
    {
        Debug.Log("LevelFinished");
        UpdateData();
        SceneManager.LoadScene("MainMenu");
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
        yield return new WaitForSeconds(10.0f);
        undoActive = false;
    }
}
