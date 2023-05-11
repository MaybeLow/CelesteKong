using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private float currentPitch = 1f;
    private bool isReversing = false;
    private bool isUndoingReverse = false;
    private static AudioManager instance;
    private AudioSource audioSource;

    // Instantiate a singleton class
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = DataManager.MusicVolume;
            audioSource.pitch = currentPitch;
            audioSource.Play();
        }
    }

    // Play sound in the direction of the current time state.
    // When time is reversed, reverse the audio
    private void FixedUpdate()
    {
        if (isReversing)
        {
            if (instance.currentPitch >= -1f)
            {
                instance.currentPitch -= Time.deltaTime;
                audioSource.pitch = currentPitch;
            }
            else
            {
                isReversing = false;
                currentPitch = -1f;
                audioSource.pitch = currentPitch;
            }
        }
        else if (isUndoingReverse)
        {
            if (instance.currentPitch <= 1f)
            {
                instance.currentPitch += Time.deltaTime;
                audioSource.pitch = currentPitch;
            }
            else
            {
                isUndoingReverse = false;
                currentPitch = 1f;
                audioSource.pitch = currentPitch;
            }
        }
    }

    public static void StopBgm()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
    }

    public static void ReverseAudio()
    {
        instance.isReversing = true;
        instance.isUndoingReverse = false;
    }

    public static void StopReverse()
    {
        instance.isReversing = false;
        instance.isUndoingReverse = true;
    }
}
