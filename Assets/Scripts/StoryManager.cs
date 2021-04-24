using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    //config params
    [SerializeField] Text textComponent;
    [SerializeField] ScriptableObject startingStory;
    [SerializeField] float fadeDelay = 2f;
    [SerializeField] float fadeSpeed = 0.1f;


    Story story;

    // Start is called before the first frame update
    void Start()
    {
        story = (Story)startingStory;
        textComponent.text = story.GetStoryText();
    }

    public void DisplayText()
    {
        var nextStory = story.GetNextStory();
        story = (Story)nextStory;
        textComponent.text = story.GetStoryText();
        StartCoroutine(TextFade());
    }

    private IEnumerator TextFade()
    {
        var tempColor = textComponent.color;
        tempColor.a = 1;
        textComponent.color = tempColor;
        yield return new WaitForSeconds(fadeDelay);
        while (tempColor.a > 0.0f)
        {
            tempColor = new Color(tempColor.r, tempColor.g, tempColor.b, tempColor.a - (Time.deltaTime / fadeSpeed));
            textComponent.color = tempColor;
            yield return null;
        }
        textComponent.text = "";

    }
}
