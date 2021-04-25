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
    [SerializeField] int lapDisplayUnlock = 3;
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
    StoryIndex storyIndex;
    Kart playerKart;
    Darker[] darker;

    private void Start()
    {
        myCamera.m_Lens.OrthographicSize = startZoomOut;
        lapCounter.SetActive(false);
        lapText.text = ("Laps: " + currentLap);
        storyManager = FindObjectOfType<StoryManager>();
        storyIndex = FindObjectOfType<StoryIndex>();
        playerKart = FindObjectOfType<Kart>();
        darker = FindObjectsOfType<Darker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerKart.CheckCheckpoints())
        {
            playerKart.ResetCheckpoints();
            this.currentLap++;
            this.lapText.text = ("Laps: " + currentLap);
            var myLap = storyIndex.GetCurrentLap(this.currentLap);
            storyManager.DisplayText();
            Unlocks();
        }
        else
        { 
            return; 
        }
    }

    private void Unlocks()
    {
        if(currentLap == lapDisplayUnlock)
        {
            lapCounter.SetActive(true);
        }
        if(currentLap % speedIncreaseUnlock == 0)
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
