using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public int timeTotal;
    public TextMeshProUGUI timerTMP;
    public int alarmScore;
    public AudioClip scoreSound;
    public AudioClip alarmSound;
    public GameObject restartText;

    private bool alarmTriggered = false;
    private int score = 0;
    private TextMeshProUGUI scoreTMP;
    private AudioSource audioSource;
    

    private void Start()
    {
        scoreTMP = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        timerTMP.text = "Time: " + Math.Max(0, timeTotal - (int)Time.timeSinceLevelLoad);
    }

    public void UpdateScore(int x)
    {
        score += x;
        DisplayScore();
        if (x > 0)
        {
            audioSource.PlayOneShot(scoreSound);
        }
    }

    public void TryTriggerAlarm()
    {
        if (!alarmTriggered)
        {
            alarmTriggered = true;
            UpdateScore(alarmScore);
            audioSource.PlayOneShot(alarmSound);
        }
    }

    private void DisplayScore()
    {
        scoreTMP.text = "Score: " + score;
    }

    public void Win()
    {
        score -= (int)Time.timeSinceLevelLoad;
        scoreTMP.text = "You Won! Score: " + score;
        restartText.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Lose()
    {
        score -= (int)Time.timeSinceLevelLoad;
        scoreTMP.text = "You Lost! Score: " + score;
        restartText.SetActive(true);
        Time.timeScale = 0f;
    }
}
