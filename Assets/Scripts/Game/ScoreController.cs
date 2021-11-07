using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text Score;
    public string title = "Score: ";
    int _score = 0;
    public Timer timer;

    public List<ScoreRule> rules = new List<ScoreRule>();
    private void Awake()
    {
        timer.SetTime(120);
        timer.OnTimeElapsed += OnGametimeElapsed;
    }

    private void OnGametimeElapsed()
    {
        MenuManager.FinishGame();
        timer.OnTimeElapsed -= OnGametimeElapsed;
    }
    int GetCompleteRule(House house, BuildingBlock block)
    {
        int result = 0;
        foreach (var rule in rules)
            if (rule.CheckRule(house, block, out result))
                break;
       
        return result;
    }    

    public void AddScore(House house, BuildingBlock block)
    {
        var score = GetCompleteRule(house, block);
        AddScore(score);
    }
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

[Serializable]
public class ScoreRule
{
    public BuildingBlockType[] parts;
    public int Score;
    public ScoreRule(List<BuildingBlockType> parts, int score)
    {
        this.parts = parts.ToArray();
        this.Score = score;
    }
    public bool CheckRule(House house, BuildingBlock block,  out int result)
    {
        result = 0;
        if (house.blocks.Count < parts.Length) return false;

        int partInd = parts.Length-1;
        var minInd = house.blocks.Count - parts.Length-1;

        for (int i = house.blocks.Count-1; i > minInd; i--, partInd--)
        {
            if ((parts[partInd] != house.blocks[i].BlockType)) 
                return false;
        }
        result = Score;
        return true;
    }
}
