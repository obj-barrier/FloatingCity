using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int score = 0;
    private TextMeshProUGUI scoreTMP;
    private AudioSource scoreSound;

    private void Start()
    {
        scoreTMP = GetComponent<TextMeshProUGUI>();
        scoreSound = GetComponent<AudioSource>();
    }

    public void Score(int x)
    {
        score += x;
        scoreTMP.text = "Score: " + score;
        scoreSound.Play();
    }
}
