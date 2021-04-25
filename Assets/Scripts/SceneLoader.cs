using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int sceneIndex;
    MusicPlayer musicPlayer;

    private void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextScene()
    {
        musicPlayer.FadeOutMusic();
        StartCoroutine(LoadDelayed());
    }

    IEnumerator LoadDelayed()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneIndex + 1);
    }
}
