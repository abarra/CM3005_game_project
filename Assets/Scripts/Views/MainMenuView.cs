using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : View
{
    [SerializeField] List<GameObject> menuList;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundFXSlider;
    [SerializeField] AudioSource onClickSound;
    [SerializeField] MuteButton masterMute;
    [SerializeField] MuteButton musicMute;
    [SerializeField] MuteButton soundFXMute;

    protected override void Start()
    {
        base.Start();
        InitSliders();
        InitMuteButtons();
    }

    #region Common listeners
    public void OnButtonPressLoadMenu(string menuName)
    {
        Debug.Log($"Loading {menuName} menu");
        //set inactive for all other sub-menus
        menuList.Where(n => n.name != menuName).ToList().ForEach(n => n.SetActive(false));
        //set active for selected sub-menu
        var activeMenu = menuList.Find(x => x.name == menuName);
        if (activeMenu == null)
        {
            var menus = new List<string>();
            menuList.ForEach(n => menus.Add(n.name));
            Debug.LogError($"Can't find menu with name {menuName}. Menus: {menus.Aggregate((a, b) => a + ", " + b)}");
        }
        else
        {
            activeMenu.SetActive(true);
        }
    }

    public void PlayButtonPressSoundEffect()
    {
        if (!onClickSound.isPlaying)
        {
            onClickSound.Play();
        }
    }
    #endregion
    
    public void StartGame()
    {
        GameManager.Instance.StartGame();
        CloseView();
    }
    
    #region Options
    public void ResetToDefaults()
    {
        PlayerPrefsManager.Instance.ResetToDefault();
        InitSliders();
        InitMuteButtons();
    }

    public void SaveOptions()
    {
        PlayerPrefsManager.Instance.Save();
    }
    
    #region Sliders
    public void ChangeMasterVol()
    {
        PlayerPrefsManager.SoundMasterSlider = masterSlider.value;
    }

    public void ChangeMusicVol()
    {
        PlayerPrefsManager.SoundMusicSlider = musicSlider.value;
    }

    public void ChangeSFXVol()
    {
        PlayerPrefsManager.SoundSfxSlider = soundFXSlider.value;
    }
    #endregion

    #region Mute buttons
    public void MuteMasterGroup(bool isMuted)
    {
        PlayerPrefsManager.SoundMasterMuted = isMuted;
    }

    public void MuteMusicGroup(bool isMuted)
    {
        PlayerPrefsManager.SoundMusicMuted = isMuted;
    }

    public void MuteSfxGroup(bool isMuted)
    {
        PlayerPrefsManager.SoundSfxMuted = isMuted;
    }
    #endregion
    
    #endregion
    
    #region Levels
    public void LoadLevelOne()
    {
        SceneManager.LoadScene(1);
        CloseView();
    }
    
    public void LoadLevelTwo()
    {
        SceneManager.LoadScene(2);
        CloseView();
    }
    #endregion

    #region Init Functions
    /// <summary>
    /// Initialise Mute Toggle Values to their saved PlayerPrefs (or to false if PlayerPrefs key/s don't exist)
    /// </summary>
    private void InitMuteButtons()
    {
        Debug.Log($"Init mute buttons. Master: {PlayerPrefsManager.SoundMasterMuted}, Music: {PlayerPrefsManager.SoundMusicMuted}, Sfx: {PlayerPrefsManager.SoundSfxMuted}");
        masterMute.SetStateWithoutNotification(PlayerPrefsManager.SoundMasterMuted);
        musicMute.SetStateWithoutNotification(PlayerPrefsManager.SoundMusicMuted);
        soundFXMute.SetStateWithoutNotification(PlayerPrefsManager.SoundSfxMuted);
    }

    /// <summary>
    /// Initialise Mixer Values to their saved PlayerPrefs (or to 0dB if PlayerPrefs key/s don't exist)
    /// </summary>
    /// <summary>
    /// Initialise Slider Values to their saved PlayerPrefs (or to 1f if PlayerPrefs key/s don't exist)
    /// </summary>
    public void InitSliders()
    {
        Debug.Log($"Init sound sliders. Master: {PlayerPrefsManager.SoundMasterSlider}, Music: {PlayerPrefsManager.SoundMusicSlider}, Sfx: {PlayerPrefsManager.SoundSfxSlider}");
        masterSlider.SetValueWithoutNotify(PlayerPrefsManager.SoundMasterSlider);
        musicSlider.SetValueWithoutNotify(PlayerPrefsManager.SoundMusicSlider);
        soundFXSlider.SetValueWithoutNotify(PlayerPrefsManager.SoundSfxSlider);
    }

    #endregion
}