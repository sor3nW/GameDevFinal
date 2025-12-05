using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{

    public Slider timerSlider;
    public TMP_Text timerText;
    public float gameTime;
    private float startTime = 0f;
    public bool stopTimer;

    public static Timer Instance { get; private set; }
    void Awake()
    {
        // Simple singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void StartTimer(int timeLimit)
    {
        gameTime = timeLimit;
        startTime = Time.time;
        stopTimer = false;
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
    }

    void Update()
    {
        if (!stopTimer)
        {
            float time = gameTime - (Time.time - startTime);

            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time - minutes * 60);

            string textTime = string.Format("{0:0}:{1:00}", minutes, seconds);

            if (time <= 0)
            {
                timerText.text = "0:00";
                timerSlider.value = 0;
                stopTimer = true;
                SceneManager.LoadScene("GameOverScene");

            }
            else if (!stopTimer)
            {
                timerText.text = textTime;
                timerSlider.value = time;
            }
        }
    }
    public void StopTimer() => stopTimer = true;
    
}