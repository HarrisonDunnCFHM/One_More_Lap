using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Goal : MonoBehaviour
{
    //config params
    [SerializeField] int currentLap = 0;
    [SerializeField] GameObject lapCounter;
    [SerializeField] Text lapText;
    [SerializeField] Text lapTextShadow;
    [SerializeField] int lapDisplayUnlock = 10;
    [SerializeField] GameObject timerCounter;
    [SerializeField] GameObject lastLapCounter;
    //[SerializeField] Text timerText;
    [SerializeField] int timerDisplayUnlock = 5;
    [SerializeField] GameObject bestTimeCounter;
    //[SerializeField] Text bestTimeText;
    [SerializeField] int bestTimeDisplayUnlock = 11;
    [SerializeField] int speedIncreaseUnlock = 5;
    [SerializeField] float speedIncrement = 1f;
    [SerializeField] float turnIncrement = 0.5f;
    [SerializeField] CinemachineVirtualCamera myCamera;
    [SerializeField] float startZoomOut = 5f;
    [SerializeField] float zoomIncrement = 1f;
    [SerializeField] float zoomOutSpeed = 0.1f;
    [SerializeField] float maxZoom = 10f;
    [SerializeField] int darkenScreenEachLevel = 5;

    //cache
    StoryManager storyManager;
    //StoryIndex storyIndex;
    Kart playerKart;
    Darker[] darker;
    LapTimer lapTimer;

    private void Start()
    {
        myCamera.m_Lens.OrthographicSize = startZoomOut;
        lapCounter.SetActive(false);
        lastLapCounter.SetActive(false);
        timerCounter.SetActive(false);
        bestTimeCounter.SetActive(false);
        lapText.text = "Laps: " + currentLap;
        lapTextShadow.text = "Laps: " + currentLap;
        storyManager = FindObjectOfType<StoryManager>();
        //storyIndex = FindObjectOfType<StoryIndex>();
        playerKart = FindObjectOfType<Kart>();
        darker = FindObjectsOfType<Darker>();
        lapTimer = FindObjectOfType<LapTimer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerKart.CheckCheckpoints())
        {
            playerKart.ResetCheckpoints();
            currentLap++;
            lapText.text = "Laps: " + currentLap;
            lapTextShadow.text = "Laps: " + currentLap;
            //this.currentLap++;
            //this.lapText.text = ("Laps: " + currentLap);
            //var myLap = storyIndex.GetCurrentLap(this.currentLap);
            storyManager.DisplayText();
            lapTimer.ResetTimer();
            Unlocks();
        }
        else
        { 
            return; 
        }
    }

    private void Unlocks()
    {
        if (currentLap == lapDisplayUnlock)
        {
            lapCounter.SetActive(true);
        }
        if (currentLap == timerDisplayUnlock)
        {
            lapTimer.StartTimer();
            timerCounter.SetActive(true);
        }
        if (currentLap == bestTimeDisplayUnlock)
        {
            bestTimeCounter.SetActive(true);
            lastLapCounter.SetActive(true);
        }
        if (currentLap % speedIncreaseUnlock == 0)
        {
            playerKart.IncreaseMoveSpeed(speedIncrement);
            playerKart.IncreaseTurnSpeed(turnIncrement);
            StartCoroutine(ZoomOut());
        }
        if (currentLap % darkenScreenEachLevel == 0)
        {
            DarkenScreen();
        }
    }

    private IEnumerator ZoomOut()
    {
        var currentZoom = myCamera.m_Lens.OrthographicSize;
        if (currentZoom <= maxZoom)
        {
            var newZoom = currentZoom + zoomIncrement;
            while (myCamera.m_Lens.OrthographicSize <= newZoom)
            {
                myCamera.m_Lens.OrthographicSize += zoomOutSpeed * Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            yield return null;
        }

    }

    private void DarkenScreen()
    {
        foreach(Darker dark in darker)
        {
            dark.DarkenImage();
        }
    }
}
