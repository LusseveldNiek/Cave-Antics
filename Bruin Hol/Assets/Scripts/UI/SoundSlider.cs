using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSlider : MonoBehaviour
{
    public Slider soundSlider;
    public static float GlobalVolume = 1f;

    private void Start()
    {
        soundSlider.onValueChanged.AddListener(UpdateSoundVolume);
    }

    public void UpdateSoundVolume(float volume)
    {
        GlobalVolume = Mathf.Clamp01(volume);

        // Update the volume of all audio sources in the scene
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = GlobalVolume;
        }
    }
}
