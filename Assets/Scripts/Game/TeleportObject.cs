using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    public Transform point;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = new Vector3(point.position.x, collision.transform.position.y, collision.transform.position.z);
    }
}
