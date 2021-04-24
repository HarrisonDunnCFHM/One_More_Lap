using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    //config params

    //cache
    ManageStory manageStory;
    Text myText;

    private void Start()
    {
        manageStory = FindObjectOfType<ManageStory>();
        //myText.text = manageStory.GetStoryText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        manageStory.DisplayText("One more lap!");
        //var storyText = story.GetStoryText();
        
    }
}
