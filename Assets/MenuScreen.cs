using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] MenuBGM bgm;

    private void OnEnable()
    {
        bgm.UpdateVolume(DataManager.MusicVolume);
    }
}
