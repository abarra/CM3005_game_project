using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : View
{
    [SerializeField] List<GameObject> menuList;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundFXSlider;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource onClickSound;
    [SerializeField] MuteButton masterMute;
    [SerializeField] MuteButton musicMute;
    [SerializeField] MuteButton soundFXMute;

    protected override void Start()
    {
        base.Start();
        InitMixerVols();
        InitSliders();
        InitToggles();
    }

    protected override void Update()
    {
        base.Update();

    }
    public void OnButtonPressLoadMenu(string menuName)
    {
        //set inactive for all other sub-menus
        menuList.Where(n => n.name != menuName).ToList().ForEach(n => n.SetActive(false));
        //set active for selected sub-menu
        GameObject activeMenu = menuList.Find(x => x.name == menuName);
        activeMenu.SetActive(true);
    }

    #region Audio Helper Functions
    void ChangeMixerGroupVol(string mixerGroupName, float value)
    {
        float toDB = Mathf.Log10(value) * 20;
        audioMixer.SetFloat(mixerGroupName, toDB);
        PlayerPrefs.SetFloat(mixerGroupName, toDB);
    }


    /// <summary>
    /// Initialise Mute Toggle Values to their saved PlayerPrefs (or to false if PlayerPrefs key/s don't exist)
    /// </summary>
    void InitToggles()
    {
        bool masterOn = false;
        bool musicOn = false;
        bool soundOn = false;
        if (!PlayerPrefs.HasKey("masterToggleValue"))
        {
            PlayerPrefs.SetInt("masterToggleValue", 0);
        }
        if (!PlayerPrefs.HasKey("musicToggleValue"))
        {
            PlayerPrefs.SetInt("musicToggleValue", 0);
        }
        if (!PlayerPrefs.HasKey("soundFXToggleValue"))
        {
            PlayerPrefs.SetInt("soundFXToggleValue", 0);
        }


        if (PlayerPrefs.GetInt("masterToggleValue") == 1)
        {
            masterOn = true;
        }
        if (PlayerPrefs.GetInt("musicToggleValue") == 1)
        {
            musicOn = true;
        }
        if (PlayerPrefs.GetInt("soundFXToggleValue") == 1)
        {
            soundOn = true;
        }

        masterMute.SetStateWithoutNotification(masterOn);
        musicMute.SetStateWithoutNotification(musicOn);
        soundFXMute.SetStateWithoutNotification(soundOn);
    }

    /// <summary>
    /// Initialise Mixer Values to their saved PlayerPrefs (or to 0dB if PlayerPrefs key/s don't exist)
    /// </summary>
    void InitMixerVols()
    {
        if (!PlayerPrefs.HasKey("MasterVol"))
        {
            PlayerPrefs.SetFloat("MasterVol", Mathf.Log10(1.0f) * 20);
        }
        if (!PlayerPrefs.HasKey("MusicVol"))
        {
            PlayerPrefs.SetFloat("MusicVol", Mathf.Log10(1.0f) * 20);
        }
        if (!PlayerPrefs.HasKey("SFXVol"))
        {
            PlayerPrefs.SetFloat("SFXVol", Mathf.Log10(1.0f) * 20);
        }

        audioMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
        audioMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
        audioMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
    }

    /// <summary>
    /// Initialise Slider Values to their saved PlayerPrefs (or to 1f if PlayerPrefs key/s don't exist)
    /// </summary>
    public void InitSliders()
    {
        if (!PlayerPrefs.HasKey("masterSliderValue"))
        {
            PlayerPrefs.SetFloat("masterSliderValue", 1f);
        }
        if (!PlayerPrefs.HasKey("musicSliderValue"))
        {
            PlayerPrefs.SetFloat("musicSliderValue", 1f);
        }
        if (!PlayerPrefs.HasKey("soundFXSliderValue"))
        {
            PlayerPrefs.SetFloat("soundFXSliderValue", 1f);
        }

        masterSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("masterSliderValue"));
        musicSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("musicSliderValue"));
        soundFXSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("soundFXSliderValue"));
    }
    void SaveSliderValue(string sliderName)
    {
        switch (sliderName)
        {
            case "masterSlider":
                PlayerPrefs.SetFloat("masterSliderValue", masterSlider.value);
                break;
            case "musicSlider":
                PlayerPrefs.SetFloat("musicSliderValue", musicSlider.value);
                break;
            case "soundFXSlider":
                PlayerPrefs.SetFloat("soundFXSliderValue", soundFXSlider.value);
                break;
            default:
                Debug.LogError($"MainMenu.cs: SaveSliderValue\nUnrecognized sliderName: {sliderName}");
                break;
        }
    }
    #endregion

    public void ChangeMasterVol()
    {
        if (!masterMute.IsMuted())
        {
            ChangeMixerGroupVol("MasterVol", masterSlider.value);
        }
        SaveSliderValue("masterSlider");
    }

    public void ChangeMusicVol()
    {
        if (!musicMute.IsMuted())
        {
            ChangeMixerGroupVol("MusicVol", musicSlider.value);
        }
        SaveSliderValue("musicSlider");
    }
    public void ChangeSFXVol()
    {
        if (!soundFXMute.IsMuted())
        {
            ChangeMixerGroupVol("SFXVol", soundFXSlider.value);
        }
        SaveSliderValue("soundFXSlider");
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
        CloseView();
    }

    public void PlayButtonPressSoundEffect()
    {
        if (!onClickSound.isPlaying)
        {
            onClickSound.Play();
        }
    }

    void MuteMixerGroup(string mixerGroupName)
    {
        ChangeMixerGroupVol(mixerGroupName, masterSlider.minValue);
    }

    public void ToggleMuteMasterGroup(bool isOn)
    {
        int toggleValue = 0;
        if (isOn)
        {
            MuteMixerGroup("MasterVol");
            toggleValue = 1;
        }
        else
        {
            ChangeMasterVol();
        }
        PlayerPrefs.SetInt("masterToggleValue", toggleValue);
    }
    public void ToggleMuteMusicGroup(bool isOn)
    {
        int toggleValue = 0;
        if (isOn)
        {
            MuteMixerGroup("MusicVol");
            toggleValue = 1;
        }
        else
        {
            ChangeMusicVol();
        }
        PlayerPrefs.SetInt("musicToggleValue", toggleValue);
    }

    public void ToggleMuteSFXGroup(bool isOn)
    {
        int toggleValue = 0;
        if (isOn)
        {
            MuteMixerGroup("SFXVol");
            toggleValue = 1;
        }
        else
        {
            ChangeSFXVol();
        }
        PlayerPrefs.SetInt("soundFXToggleValue", toggleValue);
    }
}
