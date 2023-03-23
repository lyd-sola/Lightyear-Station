using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;

    [Header("Components")]
    [SerializeField] TextMeshProUGUI textTimer;
    [SerializeField] Image progressBar;


    [Header("Events")]
    [SerializeField] IntEventChannel startGameEvent;
    [SerializeField] VoidEventChannel playerDeathEvent;
    [SerializeField] VoidEventChannel levelSuccess;

    int nowTime;
    float progress;

    private void Awake()
    {
        instance = this;

        startGameEvent.AddListener(GameStart);
        playerDeathEvent.AddListener(GameOver);

        gameObject.SetActive(false);
    }

    public void GameStart(int level)
    {
        // set timer
        nowTime = 0;
        textTimer.text = nowTime.ToString();
        textTimer.color = LevelControl.instance.nowLevelData.timerColor;

        // set progress bar
        progress = 0f;
        progressBar.fillAmount = progress;

        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    void GameOver()
    {

    }

    void Update()
    {
        UpdateTimer();
        UpdateProgressBar();
    }

    void UpdateTimer()
    {
        int time = Mathf.FloorToInt(LevelControl.instance.GameTime);
        if (time != nowTime)
        {
            nowTime = time;
            textTimer.text = nowTime.ToString();
        }
    }

    void UpdateProgressBar()
    {
        progress = LevelControl.instance.GameTime / LevelControl.instance.nowLevelData.exitGenTime;
        progressBar.fillAmount = progress;
    }
}
