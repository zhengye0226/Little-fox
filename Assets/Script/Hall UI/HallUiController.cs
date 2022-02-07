using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HallUiController : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defalutVolume = 0.5f;

    [Header("GamePlay Setting")]
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSilder = null;
    [SerializeField] private int defalutSen = 4;
    public int mainControllerSen = 4;
    
    [Header("InvertY Toggle Setting")]
    [SerializeField] private Toggle invertYToggle = null;

    [Header("Graphics Seeting")]
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private float defalutBrightness = 1;
    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;
    private int defalutResolutionValue;
    //move 10 pixel
    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown = null;

    [Header("Resolution DropDowns")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    [Header("FullScreen Toggle Setting")]
    [SerializeField] private Toggle fullScreenToggle = null;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;
    [SerializeField] public TMP_Text sucessMessage = null;

    [Header("Levels To load")]
    public string _newGameLevel;
    private string levelToload;
    [SerializeField] private GameObject noSaveGameDilog = null;
    private void Awake() {

        resolutions = GetResolution(Screen.resolutions);
        resolutionDropdown.ClearOptions();
        
    }


    /// <summary>
    /// Remove repetition resolution
    /// </summary>
    /// <param name="values">resolutionp[]</param>
    /// <returns></returns>
    public static Resolution[] GetResolution(Resolution[] values)
{

            Dictionary<string, Resolution> options = new Dictionary<string, Resolution>();

            List<Resolution> list = new List<Resolution>();

            for(int i=0; i<values.Length; i++)
            {
                string option = values[i].width + "X" + values[i].height;

                options[option] = values[i];
            }

            foreach(var item in options.Keys)
            {
                list.Add(options[item]);
            }
            return list.ToArray();

}

    private void Start()
    {
        List<string> optionList = new List<string>();

        int nowResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "X" + resolutions[i].height;

            optionList.Add(option);
        }

        if(PlayerPrefs.HasKey("masterResolution")){
            nowResolutionIndex=PlayerPrefs.GetInt("masterResolution");
        }else{
            nowResolutionIndex=defalutResolutionValue;
            PlayerPrefs.SetInt("masterResolution",nowResolutionIndex);
        }
        
        resolutionDropdown.AddOptions(optionList);
        resolutionDropdown.value=nowResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        _brightnessLevel = PlayerPrefs.GetFloat("masterBrightness");

        _qualityLevel = PlayerPrefs.GetInt("masterQuality");

        mainControllerSen = PlayerPrefs.GetInt("masterSensitivity");

    }

    /// <summary>
    /// Open the main sence
    /// </summary>
    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    /// <summary>
    /// load game data you save before
    /// </summary>
    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("SaveLevel"))
        {
            levelToload = PlayerPrefs.GetString("Savelevel");
            //Save data about game 
            //PlayerPrefs.SetString("Savelevel",yourlevelis);
            SceneManager.LoadScene(levelToload);
        }
        else {
            noSaveGameDilog.SetActive(true);
        }
    }
    /// <summary>
    /// Exit Game to desk
    /// </summary>
    public void ExitButtom() {

        Application.Quit();
    }
    /// <summary>
    /// Set volume and show it in the dialog
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = AudioListener.volume.ToString("0.0");
    }

    /// <summary>
    /// Apply sound setting into PlayerPrefs
    /// </summary>
    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        sucessMessage.text = "Sound Sucess";
        StartCoroutine(ConfirmationBox());
    }

    /// <summary>
    /// Set sensitivity value
    /// </summary>
    /// <param name="sensitivity"></param>
    public void SetControllerSen(float sensitivity)
    {
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        controllerSenTextValue.text = mainControllerSen.ToString("0");
    }

    /// <summary>
    /// Save user's GamePlayerSetting
    /// </summary>
    public void GamePlayApply()
    {
        if(invertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY",1);
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY",0);
        }

        PlayerPrefs.SetInt("masterSensitivity",mainControllerSen);
        sucessMessage.text = "GamePlay Sucess";
        StartCoroutine(ConfirmationBox());
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel=brightness;
        brightnessTextValue.text=brightness.ToString("0.0");
    }

    public void SetFullScreen(bool fullScreen)
    {
        _isFullScreen=fullScreen;
    }

    public void SetQuality(int qualityValue)
    {
        _qualityLevel=qualityValue;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
        PlayerPrefs.SetInt("masterResolution",resolutionDropdown.value);
    }
    
    public void GraphicsApply()
    {
        //Change your brightness with your post processing or whatever it is
        PlayerPrefs.SetFloat("masterBrightness",_brightnessLevel);

        //Make game interface full screen
        PlayerPrefs.SetInt("masterFullScreen",(_isFullScreen? 1 : 0));
        Screen.fullScreen=_isFullScreen;
        
        PlayerPrefs.SetInt("masterQuality",_qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);
        
        PlayerPrefs.SetInt("masterResolution",resolutionDropdown.value);


        sucessMessage.text = "Graphics Sucess";
        StartCoroutine(ConfirmationBox());
    }
    /// <summary>
    /// Back and Default
    /// </summary>
    /// <param name="volume"></param>
    public void ResetButton(string MenuType)
    {
        if(MenuType == "Audio")
        {
            AudioListener.volume = defalutVolume;
            volumeSlider.value = defalutVolume;
            volumeTextValue.text = volumeSlider.value.ToString("0.0");
            VolumeApply();
        }
        if(MenuType == "GamePlay")
        {
            controllerSenSilder.value = defalutSen;
            controllerSenTextValue.text = defalutSen.ToString("0");
            mainControllerSen = defalutSen;
            invertYToggle.isOn = false;
            GamePlayApply();
        }
        if(MenuType == "Graphics")
        {
            brightnessSlider.value=defalutBrightness;
            brightnessTextValue.text = defalutBrightness.ToString("0.0");
            
            //Default Level is low
            qualityDropdown.value=1;
            QualitySettings.SetQualityLevel(1);

            //Defalut fullScreen is false
            fullScreenToggle.isOn=false;
            Screen.fullScreen=false;
            
            Resolution currentResolution=Screen.currentResolution;
            Screen.SetResolution(currentResolution.width,currentResolution.height,Screen.fullScreen);
            resolutionDropdown.value=resolutions.Length;

            GraphicsApply();
        }
    }

    /// <summary>
    /// When the volume change sucess show the message such as "Volume Setting Sucess" 
    /// </summary>
    /// <returns></returns>
    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }
}
