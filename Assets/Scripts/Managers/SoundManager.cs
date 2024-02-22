using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    [SerializeField] private AudioSource soundFXSrc;
    [SerializeField] private AudioSource musicSrc;
    [SerializeField] AudioMixer mixer;
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
            InitMixerVols(mixer);
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        //musicSrc = GetComponent<UnityEngine.AudioSource>();
        musicSrc.PlayOneShot(levelTheme_1);
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

    public static void InitMixerVols(AudioMixer mixer)
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

        mixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
        mixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
        mixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
    }
}
