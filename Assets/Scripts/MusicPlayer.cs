using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] bool isMuted = false;
    
    private void Awake()
    {
        int musicPlayerCount = FindObjectsOfType<MusicPlayer>().Length;
        if (musicPlayerCount > 1)
        {
            Destroy(gameObject);
        }    
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }



    public void ToggleMute()
    {
        if(!isMuted)
        {
            GetComponent<AudioSource>().mute = true;
            isMuted = true;
        }
        else if(isMuted)
        {
            GetComponent<AudioSource>().mute = false;
            isMuted = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
