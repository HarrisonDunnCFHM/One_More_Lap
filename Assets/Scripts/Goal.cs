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

    //cache
    StoryManager storyManager;
    StoryIndex storyIndex;
    Kart playerKart;

    private void Start()
    {
        myCamera.m_Lens.OrthographicSize = startZoomOut;
        lapCounter.SetActive(false);
        lapText.text = ("Laps: " + currentLap);
        storyManager = FindObjectOfType<StoryManager>();
        storyIndex = FindObjectOfType<StoryIndex>();
        playerKart = FindObjectOfType<Kart>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.currentLap++;
        this.lapText.text = ("Laps: " + currentLap);
        //storyIndex.CompleteLap();
        var myLap = storyIndex.GetCurrentLap(this.currentLap);
        Debug.Log("Current lap is " + myLap);
        storyManager.DisplayText();
        Unlocks();
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
    }

    private IEnumerator ZoomOut()
    {
        var currentZoom = myCamera.m_Lens.OrthographicSize;
        var newZoom = currentZoom + zoomIncrement;
        while(myCamera.m_Lens.OrthographicSize <= newZoom)
        {
            myCamera.m_Lens.OrthographicSize += zoomOutSpeed * Time.deltaTime;
            yield return null;
        }
        
    }
}
