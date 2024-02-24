﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get => _instance;
    }

    [SerializeField] private AudioSource soundFXSrc;
    [SerializeField] private AudioSource musicSrc;

    [SerializeField] private AudioMixer mixer;

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
    public AudioClip collectableSound;


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

    public void PlayCollectableSound()
    {
        soundFXSrc.PlayOneShot(collectableSound);
        // we can randomly play  congratulation sound

        int rnd = Random.Range(0, 10);
        if (rnd >= 7)
        {
            this.PlayCongratulationsSound();
        }
    }


    private void Update()
    {
        /* TEST CODE */


        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayCollectableSound();
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