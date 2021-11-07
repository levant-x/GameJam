using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class CursorController : MonoBehaviour
{
    public event Action<BuildingBlock> OnClickBuildingBlock = delegate { };
    public event Action<Collider2D> OnClickObject = delegate { };
    public event Action OnClickEmpty = delegate { };
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
                        OnClickBuildingBlock?.Invoke(building);
                        Debug.Log("hit.collider.gameObject " + hit.collider.gameObject.name);
                    }
                    else { OnClickEmpty?.Invoke(); }
                }
                else if(hit.collider)
                {
                    OnClickObject?.Invoke(hit.collider);
                }
                else
                {
                    OnClickEmpty?.Invoke();
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
