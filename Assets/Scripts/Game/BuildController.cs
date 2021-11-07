using System;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public CursorController Cursor;
    public BuildingBlock Prefab;

    BuildingBlock selectedObject;
    public HouseController HouseController;

    private void Awake()
    {
        Cursor.OnClickBuildingBlock += Click;
        Cursor.OnClickEmpty += EmptyClick;
    }

    private void EmptyClick()
    {        
        if (selectedObject)
        {
            selectedObject.UnSetCaught();
            selectedObject = null;
        }        
    }

    private void FixedUpdate()
    {
        if (selectedObject && Input.GetMouseButton(0))
        {
            SetObjPosToCursorPos(selectedObject);
        }
        else if(selectedObject)
        {
            EmptyClick();
        }
    }

    bool TryToSetBuilding()
    {
        if (selectedObject == null) return false;
        return true;
    }

    private void Click(BuildingBlock obj)
    {
        if (HouseController.HasBlock(obj)) return;
        selectedObject = obj;
        selectedObject.SetCaught();
        return;
    }
    void SetObjPosToCursorPos(BuildingBlock gameObject)
    {
        if (gameObject == null) return;
        var setVelocity = Cursor.CalculateCurrentCursorPos() - selectedObject.transform.position;
        //Debug.Log(setVelocity);
        gameObject.myRigidBody.velocity = setVelocity*10;
        //gameObject.myRigidBody.MovePosition(Cursor.CalculateCurrentCursorPos());
    }

    private void OnDestroy()
    {
        Cursor.OnClickBuildingBlock -= Click;
    }
}
