using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Story Event")]
public class StoryEvent : ScriptableObject
{

    //config params
    [SerializeField] bool isLoopEnd;
    [SerializeField] StoryEvent loopRestartGoTo;
    [TextArea(10, 14)] [SerializeField] string storyText;
    [SerializeField] StoryEvent nextStory;

    //cache
    int nextStoryIndex;
    int myIndex;

    private void OnEnable()
    {
        
    }

    public string GetStoryText()
    {
        return storyText;
    }

    public StoryEvent GetNextStory()
    {
        if (!isLoopEnd)
        {
            var storyArray = FindObjectOfType<StoryIndex>().GetStories();
            myIndex = System.Array.IndexOf(storyArray, this);
            nextStoryIndex = myIndex + 1;
            nextStory = FindObjectOfType<StoryIndex>().GetCurrentLap(nextStoryIndex);
            return nextStory;
        }
        else 
        {
            return loopRestartGoTo;
        }

    }

    
}
