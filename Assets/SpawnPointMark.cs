using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointMark : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}
