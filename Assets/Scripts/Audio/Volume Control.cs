using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// When I first wrote this script several years ago, I used this Unity tutorial: https://www.youtube.com/watch?v=MmWLK9sN3s8
public class VolumeControl : MonoBehaviour
{
    [SerializeField] string volumeParameter = "MasterVolume";

    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Slider slider;

    [SerializeField] float multiplier = 30f;

    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    void Start()
    {
        // We may want to use the game's current saving system instead to store this.
        slider.value = PlayerPrefs.GetFloat(volumeParameter, slider.value);
    }

    private void OnDisable()
    {
        // We may want to use the game's current saving system instead to store this as well.
        PlayerPrefs.SetFloat(volumeParameter, slider.value);
    }

    private void HandleSliderValueChanged(float value)
    {
        audioMixer.SetFloat(volumeParameter, Mathf.Log10(value) * multiplier);
    }
}