using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    private AudioSource sound;

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
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<UnityEngine.AudioSource>();
        sound.PlayOneShot(levelTheme_1);
    }

    public void PlayMenuMoveSound()
    {

        sound.PlayOneShot(menuMoveSound);
    }

    public void PlayMenuSelectSound()
    {
        sound.PlayOneShot(menuSelectSound);
    }
    public void PlayTheme()
    {
        sound.PlayOneShot(levelTheme_1);
    }


    // play any sound by passing it as argument
    public void PlayASound(AudioClip clip)
    {
        Debug.Log(clip);
        sound.PlayOneShot(clip);
    }


    public void PlayOuchSound()
    {

        sound.PlayOneShot(ouchSound);
    }


    public void PlayCongratulationsSound()
    {

        sound.PlayOneShot(congratulationSound);
    }

    public void PlayHitSound()
    {

        sound.PlayOneShot(hitSound);
    }

    public void PlayCollectableSound()
    {
       
        sound.PlayOneShot(collectableSound);
        // we can randomly play  congratulation sound

        int  rnd =  Random.Range(0, 10);
        if (rnd >= 7) {

            this.PlayCongratulationsSound();


        }


    }

  
    private void Update()
    {
        /* TEST CODE */


        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayCollectableSound();
        } else if (Input.GetKeyDown(KeyCode.X)){

            PlayHitSound();
        }
        else if (Input.GetKeyDown(KeyCode.C)){

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


}
