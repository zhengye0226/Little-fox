using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoadPrefs : MonoBehaviour
{

    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("GamePlay Setting")]
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSilder = null;

    [Header("InvertY Toggle Setting")]
    [SerializeField] private Toggle invertYToggle = null;

    [Header("Graphics Seeting")]
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private Slider brightnessSlider = null;

    //move 10 pixel
    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown = null;

    [Header("Resolution DropDowns")]
    public TMP_Dropdown resolutionDropdown;

    [Header("FullScreen Toggle Setting")]
    [SerializeField] private Toggle fullScreenToggle = null;

    private void InitializePlayerPref()
    {
        if (!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 0.5f);
        }
        if (!PlayerPrefs.HasKey("masterSensitivity"))
        {
            PlayerPrefs.SetInt("masterSensitivity", 4);
        }
        if (!PlayerPrefs.HasKey("masterInvertY"))
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
        }
        if (!PlayerPrefs.HasKey("masterBrightness"))
        {
            PlayerPrefs.SetFloat("masterBrightness", 1.0f);
        }
        if (!PlayerPrefs.HasKey("masterFullScreen"))
        {
            PlayerPrefs.SetInt("masterFullScreen", 0);
        }
        if (!PlayerPrefs.HasKey("masterQuality"))
        {
            PlayerPrefs.SetInt("masterQuality", 1);
        }
        if (!PlayerPrefs.HasKey("masterResolution"))
        {
            PlayerPrefs.SetInt("masterResolution", 15);
        }
        if (!PlayerPrefs.HasKey("UpKey"))
        {
            PlayerPrefs.SetInt("UpKey", (int)KeyCode.W);
        }
        if (!PlayerPrefs.HasKey("DownKey"))
        {
            PlayerPrefs.SetInt("DownKey", (int)KeyCode.S);
        }

        if (!PlayerPrefs.HasKey("LeftKey"))
        {
            PlayerPrefs.SetInt("LeftKey", (int)KeyCode.A);
        }

        if (!PlayerPrefs.HasKey("RightKey"))
        {
            PlayerPrefs.SetInt("RightKey", (int)KeyCode.D);
        }

        if (!PlayerPrefs.HasKey("AttackKey"))
        {
            PlayerPrefs.SetInt("AttackKey", (int)KeyCode.H);
        }
    }

    private void Awake()
    {

        InitializePlayerPref();

        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        volumeTextValue.text = PlayerPrefs.GetFloat("masterVolume").ToString("0.0");

        controllerSenSilder.value = PlayerPrefs.GetInt("masterSensitivity");
        controllerSenTextValue.text = PlayerPrefs.GetInt("masterSensitivity").ToString("0");

        if (PlayerPrefs.GetInt("masterInvertY") == 0)
        {
            invertYToggle.isOn = false;
        }
        else
        {
            invertYToggle.isOn = true;
        }

        brightnessSlider.value = PlayerPrefs.GetFloat("masterBrightness");
        brightnessTextValue.text = PlayerPrefs.GetFloat("masterBrightness").ToString("0.0");

        qualityDropdown.value = PlayerPrefs.GetInt("masterQuality");

        if (PlayerPrefs.GetInt("masterFullScreen") == 0)
        {
            fullScreenToggle.isOn = false;
        }
        else
        {
            fullScreenToggle.isOn = true;
        }

        resolutionDropdown.value = PlayerPrefs.GetInt("masterResolution");
    }
}
