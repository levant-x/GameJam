using System.Collections.Generic;
using UnityEngine;

using System;

public class HouseController : MonoBehaviour
{
    public ScoreController ScoreController;
    public List<House> Houses = new List<House>();

    public bool IsFromSameHouse(BuildingBlock block, BuildingBlock colliderExitBlock)
    {
        var house = GetHouse(block);
        return HasBlock(house, colliderExitBlock);
    }

    public void RemoveBlocks(BuildingBlock block)
    {
        var house = GetHouse(block);
        house.RemoveBlocks(block);
    }    
        
    public bool HasBlock(BuildingBlock block) => Houses.Exists((x) => x.blocks.Contains(block));
    public bool HasBlock(House house, BuildingBlock block) => house.blocks.Contains(block);

    public void StartHouse(BuildingBlock block)
    {
        block.SetFoundation();
        Houses.Add(new House (block));
    }

    public void AddBlock(BuildingBlock block, BuildingBlock collidedBlock)
    {
        var house = GetHouse(collidedBlock);
        if (HasBlock(block)) return;

        if (house?.AddBlock(block) ?? false)
        {            
            ScoreController.AddScore(house, block); 
        }
    }
    House GetHouse(BuildingBlock block) => Houses.Find((x) => x.blocks.Contains(block));
}
[Serializable]
public class House
{
    public List<BuildingBlock> blocks = new List<BuildingBlock>();

    public House(BuildingBlock block)
    {
        blocks.Add(block);
    }

    public bool AddBlock(BuildingBlock block)
    {
        if (!blocks.Contains(block))
        {
            blocks.Add(block);            
            return true;
        }
        return false;
    }

    public void RemoveBlocks(BuildingBlock block)
    {
        var ind = blocks.IndexOf(block);
        blocks[ind].DestroyBlock();
        blocks.Remove(block);
    }
}
