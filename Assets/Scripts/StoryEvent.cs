using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Story Event")]
public class StoryEvent : ScriptableObject
{
    
    //config params
    [TextArea(10, 14)] [SerializeField] string storyText;
    [SerializeField] ScriptableObject nextStory;

    //cache
    int nextStoryIndex;
    int myIndex;

    private void OnEnable()
    {
        myIndex = System.Array.IndexOf(FindObjectOfType<StoryIndex>().GetStories(), this);
        nextStoryIndex = myIndex + 1;
        nextStory = FindObjectOfType<StoryIndex>().GetCurrentLap(nextStoryIndex);
    }

    public string GetStoryText()
    {
        return storyText;
    }

    public ScriptableObject GetNextStory()
    {
        return nextStory;
    }

    
}
