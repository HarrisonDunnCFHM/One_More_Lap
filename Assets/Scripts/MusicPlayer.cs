using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] bool isMuted = false;
    [SerializeField] bool gamePaused;
    //[SerializeField] float fadeSpeed = 0.1f;

    AudioSource myAudioSource;

    
    /*private void Awake()
    {
        int musicPlayerCount = FindObjectsOfType<MusicPlayer>().Length;
        if (musicPlayerCount > 1)
        {
            Destroy(gameObject);
        }    
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }*/

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        gamePaused = false;
    }

    public void FadeOutMusic()
    {
        StartCoroutine(FadeOut(myAudioSource,2f,0f));
    }

    private IEnumerator FadeOut(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime/duration);
            yield return null;
        }
        yield break;
        
    }

    public void ToggleMute()
    {
        if (!gamePaused)
        {
            if (!isMuted)
            {
                myAudioSource.mute = true;
                isMuted = true;
            }
            else if (isMuted)
            {
                myAudioSource.mute = false;
                isMuted = false;
            }
        }
        else
        { 
            return;
        }
    }

    public void ToggleMuteInput(bool toggle)
    {
        if(toggle)
        {
            myAudioSource.mute = true;
            gamePaused = true;
            isMuted = true;
        }
        else if (!toggle)
        {
            myAudioSource.mute = false;
            gamePaused = false;
            isMuted = false;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
