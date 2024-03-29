using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteListener : MonoBehaviour
{
    public GameObject levelCompletePanel;

    private void Start()
    {
        levelCompletePanel.SetActive(false);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void OnLevelComplete()
    {
        PauseGame();
        int levelId = SceneManager.GetActiveScene().buildIndex;
        if (levelId + 1 < SceneManager.sceneCountInBuildSettings)
        {
            DataManager.UnlockedLevels.Add(levelId + 1);
        }
            levelCompletePanel.SetActive(true);
    }

    public void OnNextLevelButton()
    {
        ResumeGame();
        levelCompletePanel.SetActive(false);
        GameManager.FinishCurrentLevel();
    }

    public void OnExitButton()
    {
        ResumeGame();
        levelCompletePanel.SetActive(false);
        GameManager.OnExitButton();
    }
}