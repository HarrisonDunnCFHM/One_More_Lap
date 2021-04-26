using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Goal : MonoBehaviour
{
    //config params
    [Header("Debug")]
    [SerializeField] int currentLap = 0;
    [Header("Configuration Parameters")]
    [SerializeField] float startZoomOut = 5f;
    [SerializeField] float maxZoom = 10f;
    [SerializeField] float zoomOutSpeed = 0.1f;
    [Header("Object Assignments")]
    [SerializeField] Text lapText;
    [SerializeField] Text lapTextShadow;
    [SerializeField] CinemachineVirtualCamera myCamera;
    [Header("Unlocks")]
    [SerializeField] GameObject lapDisplayCanvas;
    [SerializeField] int lapDisplayUnlock = 10;
    [SerializeField] GameObject timerCanvas;
    [SerializeField] int timerDisplayUnlock = 5;
    [SerializeField] GameObject lastLapCanvas;
    [SerializeField] GameObject bestTimeCanvas;
    [SerializeField] int bestTimeDisplayUnlock = 11;
    [SerializeField] GameObject kartHeadlights;
    [SerializeField] int lightsUnlock = 31;
    [SerializeField] GameObject quitCanvas;
    [SerializeField] int quitUnlock = 79;
    [SerializeField] int takeAwayLapCounter = 102;
    [SerializeField] int giveBackLapCounter = 111;
    [SerializeField] int takeAwayBestTimer = 111;
    [SerializeField] int giveBackBestTimer = 120;
    [Header("Increments")]
    [SerializeField] int speedIncreaseFrequency = 5;
    [SerializeField] float speedIncrement = 1f;
    [SerializeField] float turnIncrement = 0.5f;
    [SerializeField] float zoomIncrement = 1f;
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
        lapDisplayCanvas.SetActive(false);
        lastLapCanvas.SetActive(false);
        timerCanvas.SetActive(false);
        bestTimeCanvas.SetActive(false);
        quitCanvas.SetActive(false);
        lapText.text = currentLap + " Laps";
        lapTextShadow.text = currentLap + " Laps";
        storyManager = FindObjectOfType<StoryManager>();
        //storyIndex = FindObjectOfType<StoryIndex>();
        playerKart = FindObjectOfType<Kart>();
        darker = FindObjectsOfType<Darker>();
        lapTimer = FindObjectOfType<LapTimer>();
        kartHeadlights.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerKart.CheckCheckpoints())
        {
            playerKart.ResetCheckpoints();
            currentLap++;
            lapText.text = currentLap + " Laps";
            lapTextShadow.text = currentLap + " Laps";
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
            lapDisplayCanvas.SetActive(true);
        }
        if (currentLap == timerDisplayUnlock)
        {
            lapTimer.StartTimer();
            timerCanvas.SetActive(true);
        }
        if (currentLap == bestTimeDisplayUnlock)
        {
            bestTimeCanvas.SetActive(true);
            lastLapCanvas.SetActive(true);
        }
        if (currentLap % speedIncreaseFrequency == 0)
        {
            playerKart.IncreaseMoveSpeed(speedIncrement);
            playerKart.IncreaseTurnSpeed(turnIncrement);
            StartCoroutine(ZoomOut());
        }
        if (currentLap % darkenScreenEachLevel == 0)
        {
            DarkenScreen();
        }
        if (currentLap == lightsUnlock)
        {
            kartHeadlights.SetActive(true);
        }
        if (currentLap == quitUnlock)
        {
            quitCanvas.SetActive(true);
        }
        if (currentLap == takeAwayLapCounter)
        {
            lapDisplayCanvas.SetActive(false);
        }
        if (currentLap == giveBackLapCounter)
        {
            lapDisplayCanvas.SetActive(true);
        }
        if (currentLap == takeAwayBestTimer)
        {
            bestTimeCanvas.SetActive(false);
        }
        if (currentLap == giveBackBestTimer)
        {
            bestTimeCanvas.SetActive(true);
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
