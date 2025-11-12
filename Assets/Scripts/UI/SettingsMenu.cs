using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer; 
    public Slider volumeSlider;       
    public TMP_Text volumeText;      

    [Header("Graphics")]
    public TMP_Dropdown qualityDropdown; 

    void Start()
    {
        volumeSlider.value = 0.75f;
        qualityDropdown.value = QualitySettings.GetQualityLevel();

        ApplyVolume();
    }

    public void ApplyVolume()
    {
        float value = volumeSlider.value;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);

        if (volumeText != null)
        {
            int percent = Mathf.RoundToInt(value * 100);
            volumeText.text = percent + "%";
        }
    }

    public void ApplyQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
