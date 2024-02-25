using System;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    #region PlayerPrefsManager singleton initialization
    private static PlayerPrefsManager _instance;
    public static PlayerPrefsManager Instance
    {
        get { return _instance; }
    }
    
    // Need to prevent double initialization and be sure that it initialize before it needed
    private bool _initialized = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            // Init settings. Need to be initialized first
            InitializeWithDefaults();
        }
    }
    #endregion
    
    #region Master sound settings
    private const string SoundMasterVolumeKey = "SoundMasterVolume";
    private const string SoundMasterMutedKey = "SoundMasterMuted";
    public static float SoundMasterVolume
    {
        get => PlayerPrefs.GetFloat(SoundMasterVolumeKey);
        set
        {
            PlayerPrefs.SetFloat(SoundMasterVolumeKey, value);
            SoundManager.Instance.ChangeMasterVolume(SoundMasterVolume);
        }
    }

    public static float SoundMasterSlider
    {
        get => Mathf.InverseLerp(-80.0f, 0.0f, SoundMasterVolume);
        set
        {
            Debug.Log($"Set master volume to {Mathf.Lerp(-80.0f, 0.0f, Mathf.Clamp01(value))}. Slider value: {value}");
            SoundMasterVolume = Mathf.Lerp(-80.0f, 0.0f, Mathf.Clamp01(value));
        }
    }
    public static bool SoundMasterMuted
    {
        get => PlayerPrefs.GetInt(SoundMasterMutedKey) != 0;
        set
        {
            PlayerPrefs.SetFloat(SoundMasterMutedKey, value ? 1 : 0);
            if (value)
            {
                SoundManager.Instance.MuteMaster();
            }
            else
            {
                SoundManager.Instance.ChangeMasterVolume(SoundMasterVolume);
            }
        }
    }
    #endregion

    #region Music sound settings
    private const string SoundMusicVolumeKey = "SoundMusicVolume";
    private const string SoundMusicMutedKey = "SoundMusicMuted";
    public static float SoundMusicVolume
    {
        get => PlayerPrefs.GetFloat(SoundMusicVolumeKey);
        set
        {
            PlayerPrefs.SetFloat(SoundMusicVolumeKey, value);
            SoundManager.Instance.ChangeMusicVolume(value);
        }
    }
    public static float SoundMusicSlider
    {
        get => Mathf.InverseLerp(-80.0f, 0.0f, SoundMusicVolume);
        set
        {
            Debug.Log($"Set music volume to {Mathf.Lerp(-80.0f, 0.0f, Mathf.Clamp01(value))}. Slider value: {value}");
            SoundMusicVolume = Mathf.Lerp(-80.0f, 0.0f, Mathf.Clamp01(value));
        }
    }
    public static bool SoundMusicMuted
    {
        get => PlayerPrefs.GetInt(SoundMusicMutedKey) != 0;
        set
        {
            PlayerPrefs.SetFloat(SoundMusicMutedKey, value ? 1 : 0);
            if (value)
            {
                SoundManager.Instance.MuteMusic();
            }
            else
            {
                SoundManager.Instance.ChangeMusicVolume(SoundMusicVolume);
            }
        }
    }
    #endregion
    
    #region SFX sound settings
    private const string SoundSfxVolumeKey = "SoundSfxVolume";
    private const string SoundSfxMutedKey = "SoundSFXMuted";
    public static float SoundSfxVolume
    {
        get => PlayerPrefs.GetFloat(SoundSfxVolumeKey);
        set
        {
            PlayerPrefs.SetFloat(SoundSfxVolumeKey, value);
            SoundManager.Instance.ChangeSfxVolume(value);
        }
    }
    public static float SoundSfxSlider
    {
        get => Mathf.InverseLerp(-80.0f, 0.0f, SoundSfxVolume);
        set
        {
            Debug.Log($"Set sfx volume to {Mathf.Lerp(-80.0f, 0.0f, Mathf.Clamp01(value))}. Slider value: {value}");
            SoundSfxVolume = Mathf.Lerp(-80.0f, 0.0f, Mathf.Clamp01(value));
        }
    }
    public static bool SoundSfxMuted
    {
        get => PlayerPrefs.GetInt(SoundSfxMutedKey) != 0;
        set
        {
            PlayerPrefs.SetFloat(SoundSfxMutedKey, value ? 1 : 0);
            if (value)
            {
                SoundManager.Instance.MuteSfx();
            }
            else
            {
                SoundManager.Instance.ChangeSfxVolume(SoundSfxVolume);
            }
        }
    }
    #endregion

    // Initialize PlayerPrefs with default values.
    // Safe for double call
    public void InitializeWithDefaults()
    {
        // If initialized - just skip this function
        if (_initialized)
        {
            return;
        }
        
        Debug.Log($"Initialize PlayerPrefs with default values. Master: {SoundMasterVolume}, Music: {SoundMusicVolume}, SFX: {SoundSfxVolume}");
        // Master sound settings
        if (!PlayerPrefs.HasKey(SoundMasterVolumeKey))
        {
            PlayerPrefs.SetFloat(SoundMasterVolumeKey, 0.0f);
        }
        if (!PlayerPrefs.HasKey(SoundMasterMutedKey))
        {
            PlayerPrefs.SetInt(SoundMasterMutedKey, 0);
        }
        
        // Music sound settings
        if (!PlayerPrefs.HasKey(SoundMusicVolumeKey))
        {
            PlayerPrefs.SetFloat(SoundMusicVolumeKey, -20.0f);
        }
        if (!PlayerPrefs.HasKey(SoundMusicMutedKey))
        {
            PlayerPrefs.SetInt(SoundMusicMutedKey, 0);
        }
        
        // SFX sound settings
        if (!PlayerPrefs.HasKey(SoundSfxVolumeKey))
        {
            PlayerPrefs.SetFloat(SoundSfxVolumeKey, -20.0f);
        }
        if (!PlayerPrefs.HasKey(SoundSfxMutedKey))
        {
            PlayerPrefs.SetInt(SoundSfxMutedKey, 0);
        }
        
        // Save to a file manually in case
        PlayerPrefs.Save();

        // Set as initialized
        _initialized = true;
    }

    public void ResetToDefault()
    {
        PlayerPrefs.DeleteAll();
        _initialized = false;
        InitializeWithDefaults();
        Save();
        SoundManager.Instance.InitMixerVols();
    }

    public void Save()
    {
        PlayerPrefs.Save();
    }
}