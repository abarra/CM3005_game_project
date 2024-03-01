using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Audio;
using static Enums;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get => _instance;
    }

    [SerializeField] private AudioSource soundFXSrc;
    [SerializeField] private AudioSource musicSrc;
    [SerializeField] private List<AudioSource> carSoundFXSrcs;
    [SerializeField] private AudioMixer mixer;

    // Constants to prevent magic strings
    private const string MasterVolume = "MasterVol";
    private const string MusicVolume = "MusicVol";
    private const string SfxVolume = "SFXVol";

    // available Sounds
    /* menu */
    public AudioClip menuSelectSound;
    public AudioClip menuMoveSound;

    /* level  */
    public AudioClip levelTheme_1;
    public AudioClip hitSound;

    /* collectables */
    public List<AudioClip> collectableSounds;

    /* emotion  */
    public AudioClip ouchSound;
    public AudioClip congratulationSound;

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
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitMixerVols();
    }

    #region Pause and stop audio controls
    /// <summary>
    /// Stop music
    /// </summary>
    public void StopMusic()
    {
        musicSrc.Stop();
    }
    
    /// <summary>
    /// Pause music
    /// </summary>
    public void PauseMusic()
    {
        musicSrc.Pause();
    }
    
    /// <summary>
    /// Un pause music
    /// </summary>
    public void UnPauseMusic()
    {
        musicSrc.UnPause();
    }
    
    /// <summary>
    /// Stop effects
    /// </summary>
    public void StopSfx()
    {
        soundFXSrc.Stop();
        foreach (var carSoundFXSrc in carSoundFXSrcs)
        {
            carSoundFXSrc.Stop();
        }
    }
    
    /// <summary>
    /// Un pause effects
    /// </summary>
    public void PauseSfx()
    {
        soundFXSrc.Pause();
        foreach (var carSoundFXSrc in carSoundFXSrcs)
        {
            carSoundFXSrc.Pause();
        }
    }
    
    /// <summary>
    /// Un pause music
    /// </summary>
    public void UnPauseSfx()
    {
        soundFXSrc.UnPause();
        foreach (var carSoundFXSrc in carSoundFXSrcs)
        {
            carSoundFXSrc.UnPause();
        }
    }
    
    /// <summary>
    /// Stop music and effects
    /// </summary>
    public void StopMusicAndSfx()
    {
        StopMusic();
        StopSfx();
    }
    
    /// <summary>
    /// Pause music and effects
    /// </summary>
    public void PauseMusicAndSfx()
    {
        PauseMusic();
        PauseSfx();
    }
    
    /// <summary>
    /// Un pause music and effects
    /// </summary>
    public void UnPauseMusicAndSfx()
    {
        UnPauseMusic();
        UnPauseSfx();
    }
    #endregion

    public void PlayMenuMoveSound()
    {
        soundFXSrc.PlayOneShot(menuMoveSound);
    }

    public void PlayMenuSelectSound()
    {
        soundFXSrc.PlayOneShot(menuSelectSound);
    }

    public void PlayTheme()
    {
        if (musicSrc.isPlaying)
        {
            musicSrc.Stop();
        }
        musicSrc.PlayOneShot(levelTheme_1);
    }


    // play any sound by passing it as argument
    public void PlayASound(AudioClip clip)
    {
        Debug.Log(clip);
        musicSrc.PlayOneShot(clip);
    }


    public void PlayOuchSound()
    {
        soundFXSrc.PlayOneShot(ouchSound);
    }


    public void PlayCongratulationsSound()
    {
        soundFXSrc.PlayOneShot(congratulationSound);
    }

    public void PlayHitSound()
    {
        soundFXSrc.PlayOneShot(hitSound);
    }

    public void PlayCollectableSound(int index)
    {
        soundFXSrc.PlayOneShot(collectableSounds[index]);
        // we can randomly play  congratulation sound

        int rnd = Random.Range(0, 10);
        if (rnd >= 7)
        {
            this.PlayCongratulationsSound();
        }
    }
    public void PlayCarSoundByState(CarStates state, int isPressed = 0)
    {
        switch (state)
        {
            case CarStates.neutral:
                if (!carSoundFXSrcs[0].isPlaying)
                {
                    PlayCarSoundDisableOthers(0);
                }
                break;
            case CarStates.gear1:
                if (!carSoundFXSrcs[1+isPressed].isPlaying)
                {
                    PlayCarSoundDisableOthers(1 + isPressed);
                }
                break;
            case CarStates.gear2:
                if (!carSoundFXSrcs[3 + isPressed].isPlaying)
                {
                    PlayCarSoundDisableOthers(3 + isPressed);
                }
                break;
            case CarStates.gear3:
                if (!carSoundFXSrcs[5 + isPressed].isPlaying)
                {
                    PlayCarSoundDisableOthers(5 + isPressed);
                }
                break;
            case CarStates.harshTurn:
                if (!isPlayingCarSoundsIndexRange(7, 10))
                {
                    PlayCarSoundFromRandomIndex(7, 10, false);
                }
                break;
            case CarStates.brake:
                if (!isPlayingCarSoundsIndexRange(10, 12))
                {
                    PlayCarSoundFromRandomIndex(10, 12);
                    carSoundFXSrcs[0].Play();

                }
                break;
            default:
                break;
        }
    }
    void PlayCarSoundFromRandomIndex(int indexMin, int indexMaxExclusive, bool disable = true)
    {
        int index = Random.Range(indexMin, indexMaxExclusive);
        if (disable)
        {
            PlayCarSoundDisableOthers(index);
        }
        else
        {
            carSoundFXSrcs[index].Play();
        }
    }
    bool isPlayingCarSoundsIndexRange(int indexStart, int indexEndExclusive)
    {
        bool playing = false;
        for (int i = indexStart; i < indexEndExclusive; i++)
        {
            if (carSoundFXSrcs[i].isPlaying)
            {
                playing = true; break;
            }
        }
        return playing;
    }
    void PlayCarSoundDisableOthers(int indexPlay)
    {
        for (int i = 0; i < carSoundFXSrcs.Count; i++)
        {
            if (i != indexPlay)
            {
                carSoundFXSrcs[i].Stop();
            }
        }
        carSoundFXSrcs[indexPlay].Play();
    }
    private void Update()
    {
        /* TEST CODE */


        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayCollectableSound(0);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            PlayHitSound();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            PlayMenuMoveSound();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            PlayMenuSelectSound();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            PlayOuchSound();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            PlayCongratulationsSound();
        }
        /*
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            IncreaseSatisfuction(10);

        }
       */
        /**    **/
    }

    public void InitMixerVols()
    {
        PlayerPrefsManager.Instance.InitializeWithDefaults();

        Debug.Log(
            $"Init mixer with values. Master: {PlayerPrefsManager.SoundMasterVolume}, Music: {PlayerPrefsManager.SoundMusicVolume}, SFX: {PlayerPrefsManager.SoundSfxVolume}");
        mixer.SetFloat(MasterVolume, PlayerPrefsManager.SoundMasterVolume);
        mixer.SetFloat(MusicVolume, PlayerPrefsManager.SoundMusicVolume);
        mixer.SetFloat(SfxVolume, PlayerPrefsManager.SoundSfxVolume);
    }

    // Methods to control volume. Used in PlayerPrefsManager

    #region Volume updates

    public void ChangeMasterVolume(float newVolume)
    {
        if (!PlayerPrefsManager.SoundMasterMuted)
        {
            mixer.SetFloat(MasterVolume, newVolume);
        }
    }

    public void MuteMaster()
    {
        mixer.SetFloat(MasterVolume, -80.0f);
    }

    public void ChangeMusicVolume(float newVolume)
    {
        if (!PlayerPrefsManager.SoundMusicMuted)
        {
            mixer.SetFloat(MusicVolume, newVolume);
        }
    }

    public void MuteMusic()
    {
        mixer.SetFloat(MusicVolume, -80.0f);
    }

    public void ChangeSfxVolume(float newVolume)
    {
        if (!PlayerPrefsManager.SoundSfxMuted)
        {
            mixer.SetFloat(SfxVolume, newVolume);
        }
    }

    public void MuteSfx()
    {
        mixer.SetFloat(SfxVolume, -80.0f);
    }

    #endregion Volume updates
}