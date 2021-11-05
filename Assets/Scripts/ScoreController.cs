using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TMP_Text Score;
    public string title = "Score: ";
    int _score = 0;
    public void AddScore(int score = 1)
    {
        _score += score;
        UpdateScore();
    }

    public void UpdateScore()
    {
        Score.text = title + _score;
    }
}
