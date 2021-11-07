using System;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public event Action<BuildingBlock> OnBuildingBlockClick = delegate { };
    public event Action<Collider2D> OnObjectClick = delegate { };
    public event Action OnEmptyClick = delegate { };
    Vector3 currentCursorPoint;

    public float DelayBetweenClickOnCloud { get; private set; }

    public void ResetTimerOnClick()
    {
        DelayBetweenClickOnCloud = 0;
    }

    void Update()
    {
        DelayBetweenClickOnCloud += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(CalculateCurrentCursorPos(), Vector2.zero);
            if (hit)
            {
                if (hit.collider.gameObject.TryGetComponent(out BuildingBlock building))
                {
                    if (!building.IsCaught)
                    {
                        OnBuildingBlockClick?.Invoke(building);
                        Debug.Log("hit.collider.gameObject " + hit.collider.gameObject.name);
                    }
                    else { OnEmptyClick?.Invoke(); }
                }
                else if(hit.collider)
                {
                    OnObjectClick?.Invoke(hit.collider);
                }
                else
                {
                    OnEmptyClick?.Invoke();
                }
            }
            else
            {
                OnEmptyClick?.Invoke();
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
