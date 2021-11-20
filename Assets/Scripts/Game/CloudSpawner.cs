using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using MurphyInc;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CloudSpawner : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] Prefab;
    float time;
    public BuildingBlock template;
    public CursorController CursorController;
    public Collider2D myCollider;
    public Transform spawnPoint;
    public Transform blockContainer;
    public SpriteRenderer Cloud;
    public Animator CloudExplode;
    public float AnimationDuration = 0.3f;
    public float AnimationUnhideDuration = 0.3f;
    public float DropDelay = 1.5f;
    public float ResetDelay = 1.5f;
    public Vector3 WindDirection;

    SoundsManager soundsManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }
    private void Awake()
    {
        soundsManager = FindObjectOfType<SoundsManager>();
    }

    private void OnClick()
    {
        if (CursorController.DelayBetweenClickOnCloud < DropDelay) return;
        CursorController.ResetTimerOnClick();
        myCollider.enabled = false;
        template.transform.SetParent(blockContainer);
        template.WakeUp();
        soundsManager.PlayCloudExplode();
        CloudExplode.Play("CloudExplode");
        StartCoroutine(Hide());
        template = null;
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(AnimationDuration);
        Cloud.DOFade(0, 0);
        yield return new WaitForSeconds(ResetDelay - AnimationDuration - AnimationUnhideDuration);
        Cloud.DOFade(1, AnimationUnhideDuration);
    }

    void Update()
    {
        if(time > ResetDelay)
        {
            CloudExplode.gameObject.SetActive(true);
            int ind = Random.Range(0, Prefab.Length);
            var temp = Instantiate(Prefab[ind], spawnPoint);
            template = temp.GetComponent<BuildingBlock>();
            template.Sleep();
            
            myCollider.enabled = true;
            time = 0;
        }
        if(!template)
            time += Time.deltaTime;

        transform.position += WindDirection;
    }
}

