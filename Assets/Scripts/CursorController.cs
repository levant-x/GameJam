using System;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public event Action<BuildingBlock> OnClickObj = delegate { };
    public event Action OnClickEmpty = delegate { };
    Vector3 currentCursorPoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(CalculateCurrentCursorPos(), Vector2.zero);
            if (hit)
            {                
                if (hit.collider.gameObject.TryGetComponent(out BuildingBlock building))
                {
                    if (!building.IsCaught)
                    {
                        OnClickObj?.Invoke(building);
                        Debug.Log("hit.collider.gameObject " + hit.collider.gameObject.name);
                    }                    
                    else { OnClickEmpty?.Invoke(); }
                }
            }
            else
            {
                OnClickEmpty?.Invoke();
            }
        }
    }
        
    public Vector3 CalculateCurrentCursorPos()
    {
        currentCursorPoint = Input.mousePosition;
        currentCursorPoint.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(currentCursorPoint);
    }
}
