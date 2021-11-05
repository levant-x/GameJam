using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationController : MonoBehaviour
{
    public HouseController HouseController;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BuildingBlock startBlock))
        {
            if (HouseController.HasBlock(startBlock))
            {
                HouseController.RemoveBlocks(startBlock);
                return;
            }

            if(!startBlock.IsFoundation)
                HouseController.StartHouse(startBlock);
        }
    }
}
