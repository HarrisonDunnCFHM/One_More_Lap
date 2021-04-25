using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryIndex : MonoBehaviour
{
    //config params
    [SerializeField] StoryEvent[] lapEvents;
    

    public StoryEvent[] GetStories()
    {
        return lapEvents;
    }

    public StoryEvent GetCurrentLap(int currentLap)
    {
        return lapEvents[currentLap];
    }
    
}
