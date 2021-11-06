using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TMP_Text Score;
    public string title = "Score: ";
    int _score = 0;

    ScoreRule first;

    private void Awake()
    {
        var firstlist = new List<PartOfRule> { new PartOfRule(BuildingBlockType.Ghost), 
                                               new PartOfRule(BuildingBlockType.Ghost) };
        first = new ScoreRule(firstlist, 5);
    }

    int GetCompleteRule(House house, BuildingBlock block)
    {
        int result = 0;
        first.CheckRule(house, block, out result);
        Debug.Log("result " + result + " blockname: " + block.name);
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

public class PartOfRule
{
    BuildingBlockType type;
    public PartOfRule(BuildingBlockType type)
    {
        this.type = type;
    }        
    public bool IsComplete(BuildingBlockType blockType) => type == blockType;    
}

public class ScoreRule
{
    PartOfRule[] parts;
    public int score;
    public ScoreRule(List<PartOfRule> parts, int score)
    {
        this.parts = new PartOfRule[parts.Count];
        parts.CopyTo(this.parts);
        this.score = score;
    }
    public bool CheckRule(House house, BuildingBlock block,  out int result)
    {
        bool isComplete = false;
        result = 0;

        var firstBlock = house.blocks.Find((_block) => parts[0].IsComplete(_block.BlockType));
        if (firstBlock == null) return false;

        var sameBlocksCount = house.blocks.Count((_block) => _block.BlockType == block.BlockType);

        var startInd = house.blocks.IndexOf(firstBlock);
        for (int i = 0; i < sameBlocksCount; i++)
        {
            isComplete = CheckCompleteFromInd(house, block, startInd);
            if (isComplete) break;
            //переходим к следующему похожему блоку
            startInd = house.blocks.IndexOf(firstBlock, startInd);
        }
        if (isComplete) result = score;
        return isComplete;
    }

    bool CheckCompleteFromInd(House house, BuildingBlock block, int startInd)
    {
        int partInd = 0;
        for (int i = startInd; i < house.blocks.Count; i++, partInd++)
        {
            //если блоков в строение меньше, чем в правиле.
            if (house.blocks.Count - parts.Length < 0) return false;
            //проверка выполнилось ли часть правила
            if (!parts[partInd].IsComplete(house.blocks[i].BlockType)) return false;
        }
        return partInd == parts.Length;
    }
}
