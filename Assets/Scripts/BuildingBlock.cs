using System;
using UnityEngine;

public class BuildingBlock : MonoBehaviour
{
    public event Action OnDestroyAction;
    
    public CollisionChecker CollisionChecker;

    public bool IsColliding { get; private set; }
    public bool IsCaught { get; private set; }
    public bool IsFoundation { get; set; }
    Collider2D myCollider;
    public Rigidbody2D myRigidBody;
    HouseController HouseController;

    private void Awake()
    {
        WaitInit();
        CollisionChecker.IsCollide += ChangeAreaHiglight;
        HouseController = FindObjectOfType<HouseController>();
    }

    public void Sleep()
    {
        myCollider.enabled = false;
        myRigidBody.simulated = false;
    }

    public void WakeUp()
    {
        myCollider.enabled = true;
        myRigidBody.simulated = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out BuildingBlock building))
        {
            if (!building.IsCaught)
            {
                HouseController.AddBlock(this, building);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BuildingBlock building))
        {
            //if (HouseController.IsFromSameHouse(this, building))
            {
                //HouseController.RemoveBlocks(this);
            }
        }
    }

    public void DestroyBlock()
    {
        Destroy(gameObject);
        OnDestroyAction?.Invoke();
    }

    public void SetFoundation()
    {
        IsFoundation = true;
    }

    void WaitInit()
    {
        myCollider = GetComponent<Collider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    public void SetCaught()
    {
        IsCaught = true;
        myRigidBody.freezeRotation = true;
        myRigidBody.velocity = Vector2.zero;
    }

    public void UnSetCaught()
    {
        IsCaught = false;
        myRigidBody.freezeRotation = false;
        myRigidBody.velocity = Vector2.zero;
    }

    public void ChangeAreaHiglight(bool IsCollide)
    {
        IsColliding = IsCollide;
    }

    private void OnDestroy()
    {
        CollisionChecker.IsCollide -= ChangeAreaHiglight;
    }
}