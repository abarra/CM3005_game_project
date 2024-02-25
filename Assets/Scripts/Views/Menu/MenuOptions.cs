using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundFXSlider;
    
    [SerializeField] MuteButton masterMute;
    [SerializeField] MuteButton musicMute;
    [SerializeField] MuteButton soundFXMute;
    
    /// <summary>
    /// Unity behavior Start function
    /// </summary>
    private void Start()
    {
        InitSliders();
        InitMuteButtons();
    }
    
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
    private void InitSliders()
    {
        Debug.Log($"Init sound sliders. Master: {PlayerPrefsManager.SoundMasterSlider}, Music: {PlayerPrefsManager.SoundMusicSlider}, Sfx: {PlayerPrefsManager.SoundSfxSlider}");
        masterSlider.SetValueWithoutNotify(PlayerPrefsManager.SoundMasterSlider);
        musicSlider.SetValueWithoutNotify(PlayerPrefsManager.SoundMusicSlider);
        soundFXSlider.SetValueWithoutNotify(PlayerPrefsManager.SoundSfxSlider);
    }
    #endregion
    
    /// <summary>
    /// Reset options to default value
    /// </summary>
    public void ResetToDefaults()
    {
        PlayerPrefsManager.Instance.ResetToDefault();
        InitSliders();
        InitMuteButtons();
    }

    /// <summary>
    /// Save options into prefs file
    /// </summary>
    public void SaveOptions()
    {
        PlayerPrefsManager.Instance.Save();
    }
    
    #region Sliders
    /// <summary>
    /// Master volume change listener
    /// </summary>
    public void ChangeMasterVol()
    {
        PlayerPrefsManager.SoundMasterSlider = masterSlider.value;
    }

    /// <summary>
    /// Music volume change listener
    /// </summary>
    public void ChangeMusicVol()
    {
        PlayerPrefsManager.SoundMusicSlider = musicSlider.value;
    }

    /// <summary>
    /// Sfx volume change listener
    /// </summary>
    public void ChangeSFXVol()
    {
        PlayerPrefsManager.SoundSfxSlider = soundFXSlider.value;
    }
    #endregion

    #region Mute buttons
    /// <summary>
    /// Master mute button change listener
    /// </summary>
    /// <param name="isMuted">true if muted</param>
    public void MuteMasterGroup(bool isMuted)
    {
        PlayerPrefsManager.SoundMasterMuted = isMuted;
    }

    /// <summary>
    /// Music mute button change listener
    /// </summary>
    /// <param name="isMuted">true if muted</param>
    public void MuteMusicGroup(bool isMuted)
    {
        PlayerPrefsManager.SoundMusicMuted = isMuted;
    }

    /// <summary>
    /// Sfx mute button change listener
    /// </summary>
    /// <param name="isMuted">true if muted</param>
    public void MuteSfxGroup(bool isMuted)
    {
        PlayerPrefsManager.SoundSfxMuted = isMuted;
    }
    #endregion
}
