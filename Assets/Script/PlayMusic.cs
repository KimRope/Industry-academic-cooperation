using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioSource audioMusic;
    public AudioSource ringMusic;
    public AudioSource btnMusic;
    bool MusicStart = false;
    // Start is called before the first frame update
    void Start()
    {
        audioMusic.Stop();
        MusicStart = false;
    }

    public void MusicBtn()
    {
        audioMusic.Play();
        MusicStart = true;
    }

    public void RingSound()
    {
        ringMusic.Play();
    }

    public void ButtonSound()
    {
        btnMusic.Play();
    }
}
