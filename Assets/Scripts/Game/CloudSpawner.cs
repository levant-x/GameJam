using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] Prefab;
    float time;
    public BuildingBlock template;
    public CursorController CursorController;
    public Collider2D myCollider;
    public Transform spawnPoint;
    public float DropDelay = 1.5f;
    public float ResetDelay = 1f;
    private void Awake()
    {
        CursorController.OnClickObject += OnClick;
    }

    private void OnClick(Collider2D collider)
    {
        if (myCollider != collider) return;
        if (CursorController.DelayBetweenClickOnCloud < DropDelay) return;
        CursorController.ResetTimerOnClick();
        myCollider.enabled = false;
        template.WakeUp();
        template = null;
    }

    void Update()
    {
        if(time > ResetDelay)
        {
            int ind = Random.Range(0, Prefab.Length);
            var temp = Instantiate(Prefab[ind], spawnPoint);
            template = temp.GetComponent<BuildingBlock>();
            template.Sleep();
            myCollider.enabled = true;
            time = 0;
        }
        if(!template)
            time += Time.deltaTime;
    }

    private void OnDestroy()
    {
        CursorController.OnClickObject -= OnClick;
    }
}

