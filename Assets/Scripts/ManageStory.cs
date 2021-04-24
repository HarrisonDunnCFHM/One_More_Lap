using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageStory : MonoBehaviour
{
    //config params
    [SerializeField] Text textComponent;

    float textAlpha = 1f;


    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = "Testing text goes here!";
    }

    public void DisplayText(string storyText)
    {
        textComponent.text = storyText;
        StartCoroutine(TextFade());
    }

    private IEnumerator TextFade()
    {
        yield return new WaitForSeconds(3);
        textComponent.text = "";

    }
}
