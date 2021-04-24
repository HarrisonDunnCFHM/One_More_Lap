using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    //config params
    [SerializeField] int currentLap = 0;
    [SerializeField] GameObject lapCounter;
    [SerializeField] Text lapText;
    [SerializeField] int lapDisplayUnlock = 3;

    //cache
    StoryManager storyManager;
    StoryIndex storyIndex;

    private void Start()
    {
        lapCounter.SetActive(false);
        lapText.text = ("Laps: " + currentLap);
        storyManager = FindObjectOfType<StoryManager>();
        storyIndex = FindObjectOfType<StoryIndex>();
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
        if(currentLap == 3)
        {
            lapCounter.SetActive(true);
        }
    }
}
