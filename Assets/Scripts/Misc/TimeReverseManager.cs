using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeReverseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.DisableUndo();
    }
}
