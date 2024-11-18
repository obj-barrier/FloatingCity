using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public int score;

    private ScoreKeeper scoreKeeper;

    private void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void Win()
    {
        scoreKeeper.UpdateScore(score);
        scoreKeeper.Win();
        Destroy(gameObject);
    }
}
