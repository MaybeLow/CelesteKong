using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private MenuBGM bgm;

    private float musicVolume;
    private float soundEffectsVolume;

    private void OnEnable()
    {
        musicSlider.value = DataManager.MusicVolume;
        soundSlider.value = DataManager.SoundVolume;
    }

    public void OnMusicSliderChange(float value)
    {
        musicVolume = value;
    }

    public void OnSoundEffectsSliderChange(float value)
    {
        soundEffectsVolume = value;
    }

    public void OnMenuBackButton()
    {
        DataManager.MusicVolume = musicVolume;
        DataManager.SoundVolume = soundEffectsVolume;
        SaveData.SaveGameData();
        bgm.UpdateVolume(DataManager.MusicVolume);
    }
}
