using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryIndex : MonoBehaviour
{
    //config params
    [SerializeField] ScriptableObject[] lapEvents;
    

    public ScriptableObject[] GetStories()
    {
        return lapEvents;
    }

    public ScriptableObject GetCurrentLap(int currentLap)
    {
        return lapEvents[currentLap];
    }
    
}
