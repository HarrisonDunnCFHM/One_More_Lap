using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Darker : MonoBehaviour
{
    [SerializeField] float darkenRate = 0.1f;


    Tilemap myRenderer;

    private void Start()
    {
        myRenderer = GetComponent<Tilemap>();
    }

    public void DarkenImage()
    {
        myRenderer.color = new Color (myRenderer.color.r, myRenderer.color.g - darkenRate, myRenderer.color.b - darkenRate);
    }
}
