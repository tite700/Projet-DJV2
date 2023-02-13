using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerTitle : MonoBehaviour
{ 
    private AudioSource bgMusic;
    private AudioClip Clip;
    private bool isPlayingMusic1 = true;

    private void Awake()
    {
        bgMusic = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MusicFadeIn(bgMusic));
    }

    private IEnumerator MusicFadeIn(AudioSource music)
    {

        float timeToFade = 5f;
        float timeElapsed = 0f;

        music.Play();
        while (timeElapsed < timeToFade)
        {
            music.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
    }
    
    private IEnumerator MusicFadeIn(AudioSource music1, AudioSource music2)
    {

        float timeToFade = 5f;
        float timeElapsed = 0f;

        if (isPlayingMusic1)
        {
            music2.Play();
            while (timeElapsed < timeToFade)
            {
                music2.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
            }
            music1.Stop();
        }
        else
        {
            music1.Play();
            music2.Stop();
        }
        
        yield return null;
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
