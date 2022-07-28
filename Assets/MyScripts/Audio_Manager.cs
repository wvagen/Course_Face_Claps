using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public AudioSource myAudioSource;

    public AudioClip music;
    public AudioClip tahwidaSFX, slapSFX;
    public AudioClip[] crackSFXs;

    static Audio_Manager audioInstance;
    public static bool isSoundOn;

    void Awake()
    {
        if (audioInstance == null)
        {
            audioInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        myAudioSource.mute = !isSoundOn;
    }

    public void Play_Crack_Sounds(int crackLevel)
    {
        if (isSoundOn)
        {
            myAudioSource.PlayOneShot(crackSFXs[crackLevel]);
        }
    }

    public void Play_Slap_SFX()
    {
        if (isSoundOn)
        {
            myAudioSource.PlayOneShot(slapSFX);
        }
    }

    public void Play_Tahwida_SFX()
    {
        if (isSoundOn)
        {
            myAudioSource.PlayOneShot(tahwidaSFX);
        }
    }



}
