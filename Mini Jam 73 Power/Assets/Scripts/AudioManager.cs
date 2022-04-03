using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] sounds;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    

    public void PlayButtonSound()
    {
        sounds[0].Play();
    }

    public void PLayCoinSound()
    {
        sounds[1].Play();
    }

    public void PlayDieSound()
    {
        sounds[2].Play();
    }

    public void PlayHurtSound()
    {
        sounds[3].Play();
    }

    public void PlayShootingSound()
    {
        sounds[4].Play();
    }

    public void PlayBuySound()
    {
        sounds[5].Play();
    }
}
