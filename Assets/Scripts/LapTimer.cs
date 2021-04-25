using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimer : MonoBehaviour
{
    //config params
    [SerializeField] Text timerText;
    [SerializeField] Text timerTextShadow;
    [SerializeField] Text bestTimeText;
    [SerializeField] Text bestTimeTextShadow;
    [SerializeField] Text lastLapText;
    [SerializeField] Text lastLapTextShadow;
    bool isTiming;
    float timer;

    //ref params
    float bestLap;

    // Start is called before the first frame update
    void Start()
    {
        bestLap = 0f;
        isTiming = false;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTiming)
        {
            timer += Time.deltaTime;
        }
        var timerAsText = FormatTime(timer);
        timerText.text = timerAsText;
        timerTextShadow.text = timerAsText;
    }

    public void UpdateBestTime(float currentTime)
    {
        float lapTimed = currentTime;
        if (bestLap == 0f)
        {
            bestLap = lapTimed;
            var bestLapText = FormatTime(bestLap);
            bestTimeText.text = bestLapText + " - BEST";
            bestTimeTextShadow.text = bestLapText + " - BEST";
        }
        if (lapTimed < bestLap)
        {
            bestLap = lapTimed;
            var bestLapText = FormatTime(bestLap);
            bestTimeText.text = bestLapText + " - BEST";
            bestTimeTextShadow.text = bestLapText + " - BEST";

        }
    }

    public void UpdateLastLap(float currenTime)
    {
        float lapTimed = currenTime;
        lastLapText.text = FormatTime(lapTimed) + " - Last";
        lastLapTextShadow.text = FormatTime(lapTimed) + " - Last";

    }

    public void StartTimer()
    {
        isTiming = true;
    }

    public void ResetTimer()
    {
        UpdateLastLap(timer);
        UpdateBestTime(timer);
        timer = 0f;
    }

    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        int milliseconds = (int)(1000 * (time - minutes * 60 - seconds));
        return string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }
}
