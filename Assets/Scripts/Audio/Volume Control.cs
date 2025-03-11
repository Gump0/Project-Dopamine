using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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
        slider.value = PlayerPrefs.GetFloat(volumeParameter, slider.value);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, slider.value);
    }

    private void HandleSliderValueChanged(float value)
    {
        audioMixer.SetFloat(volumeParameter, Mathf.Log10(value) * multiplier);
    }
}