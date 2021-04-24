using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    //config params
    [SerializeField] int currentLap = 0;
    //cache
    StoryManager storyManager;
    StoryIndex storyIndex;

    private void Start()
    {
        currentLap = 0;
        storyManager = FindObjectOfType<StoryManager>();
        storyIndex = FindObjectOfType<StoryIndex>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.currentLap++;
        //storyIndex.CompleteLap();
        var myLap = storyIndex.GetCurrentLap(this.currentLap);
        Debug.Log("Current lap is " + myLap);
        storyManager.DisplayText();       
    }
}
