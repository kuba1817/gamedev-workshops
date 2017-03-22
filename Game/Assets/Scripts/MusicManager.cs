using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : Singleton<MusicManager> { 

    public AudioClip backgroundMusic;

    private AudioSource musicPlayer;

    new void Awake()
    {
        musicPlayer = GetComponent<AudioSource>();
    }

    void Start()
    {
        musicPlayer.clip = backgroundMusic;
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        musicPlayer.Play();
    }

    public void StopPlayingBackgroundMusic()
    {
        musicPlayer.Stop();
    }
}
