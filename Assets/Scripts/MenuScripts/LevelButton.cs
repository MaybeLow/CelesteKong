using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Sprite lockSprite;
    [SerializeField] private Sprite unlockSprite;
    [SerializeField] private int levelId;
    private SpriteRenderer lockImage;
    private MainMenu menu;

    private void Awake()
    {
        lockImage = GetComponent<SpriteRenderer>();

        menu = GetComponentInParent<MainMenu>();
    }

    private void OnEnable()
    {
        if (DataManager.UnlockedLevels.Contains(levelId))
        {
            lockImage.sprite = unlockSprite;
        } 
        else
        {
            lockImage.sprite = lockSprite;
        }
    }

    private void OnMouseDown()
    {
        if (DataManager.UnlockedLevels.Contains(levelId))
        {
            menu.OnLevelSelectionButton(levelId);
        }
    }
}
