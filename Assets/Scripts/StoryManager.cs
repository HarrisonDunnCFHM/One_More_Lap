using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    //config params
    [SerializeField] Text storyText;
    [SerializeField] Text storyTextShadow;
    [SerializeField] StoryEvent startingStory;
    [SerializeField] float fadeDelay = 2f;
    [SerializeField] float fadeSpeed = 0.1f;


    StoryEvent story;

    // Start is called before the first frame update
    void Start()
    {
        story = startingStory;
        storyText.text = story.GetStoryText();
        storyTextShadow.text = story.GetStoryText();
    }

    public void DisplayText()
    {
        var nextStory = story.GetNextStory();
        story = nextStory;
        storyText.text = story.GetStoryText();
        storyTextShadow.text = story.GetStoryText();
        StartCoroutine(TextFade());
    }

    private IEnumerator TextFade()
    {
        var tempColor = storyText.color;
        var tempShadow = storyTextShadow.color;
        tempColor.a = 1;
        tempShadow.a = 1;
        storyText.color = tempColor;
        storyTextShadow.color = tempShadow;
        yield return new WaitForSeconds(fadeDelay);
        while (tempColor.a > 0.0f)
        {
            tempColor = new Color(tempColor.r, tempColor.g, tempColor.b, tempColor.a - (Time.deltaTime / fadeSpeed));
            tempShadow = new Color(tempShadow.r, tempShadow.g, tempShadow.b, tempShadow.a - ((Time.deltaTime / fadeSpeed)));
            storyText.color = tempColor;
            storyTextShadow.color = tempShadow;
            yield return null;
        }
        storyText.text = "";
        storyTextShadow.text = "";

    }
}
