using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneModify : MonoBehaviour
{
    private void Start()
    {
        //float rand = Random.Range(0f, 1f);
        //if(rand > 0.5f)
            transform.Rotate(Vector3.forward, 90f);
    }
}
