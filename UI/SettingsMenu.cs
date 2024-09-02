using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    public Button backButton;
    public Toggle soundToggle;
    public Slider volumeSlider;
    public AudioMixer audioMixer; // Reference to the AudioMixer in your scene
    public Image soundImage;
    public Sprite fullSoundSprite;
    public Sprite noSoundSprite;
    private void Start()
    {
        backButton.onClick.AddListener(ReturnToMainMenu);
        soundToggle.onValueChanged.AddListener(ToggleSound);
        
    }

    public void SetMasterVolume()
    {
        float volume = volumeSlider.value;
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        if(volume <= 1e-05)
        {
            soundImage.sprite = noSoundSprite;
        }
        else
        {
            soundImage.sprite = fullSoundSprite;
        }
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("0MainMenu"); // Replace "MainMenu" with the name of your main menu scene.
    }

    private void ToggleSound(bool isOn)
    {
        if (isOn)
        {
            audioMixer.SetFloat("Music", 0f); // Set the volume parameter to 0 (full volume)
          
        }
        else
        {
            audioMixer.SetFloat("Music", -80f); // Set the volume parameter to -80 (mute)
          
        }
    }

}
