using System;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public event Action<bool> IsCollide;
    int collisionsCount = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        collisionsCount++;
        IsCollide?.Invoke(true);
    }

    private void OnTriggerExit(Collider other)
    {
        collisionsCount--;
        if(collisionsCount <= 0)
        {
            collisionsCount = 0;
            IsCollide?.Invoke(false);
        }
    }
}
