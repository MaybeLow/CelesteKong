using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBGM : MonoBehaviour
{
    private AudioSource backgroundMusic;

    private void Awake()
    {
        backgroundMusic = GetComponent<AudioSource>();
    }

    private void Start()
    {
        backgroundMusic.Play();
    }

    public void UpdateVolume(float volume)
    {
        backgroundMusic.volume = volume;
    }
}
