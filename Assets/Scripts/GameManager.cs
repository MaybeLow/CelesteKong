using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private static bool undoActive { get; set; } = false;

    public void Awake()
    {
        Instance = this;
    }

    public static bool UndoActive()
    {
        return undoActive;
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
