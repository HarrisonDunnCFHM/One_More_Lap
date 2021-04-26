using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject pauseOverlay;
    
    int sceneIndex;
    MusicPlayer musicPlayer;
    bool isPaused;
    Kart playerKart;

    private void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        playerKart = FindObjectOfType<Kart>();
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        isPaused = false;
        if (pauseOverlay == null)
        { return; }
        else { pauseOverlay.SetActive(false); }
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

    public void QuitToStart()
    {
        SceneManager.LoadScene(0);
    }

    public void TogglePause()
    {
        if (!isPaused)
        {
            pauseOverlay.SetActive(true);
            isPaused = true;
            musicPlayer.ToggleMuteInput(isPaused);
            playerKart.ToggleMuteInput(isPaused);
            Time.timeScale = 0;
        }
        else if (isPaused)
        {
            pauseOverlay.SetActive(false);
            isPaused = false;
            musicPlayer.ToggleMuteInput(isPaused);
            playerKart.ToggleMuteInput(isPaused);
            Time.timeScale = 1;
        }
    }
}
