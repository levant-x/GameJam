using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Crow : MonoBehaviour, IPointerClickHandler
{
    public SpriteRenderer mySprite;
    public bool IsLaunch;
    public bool IsReturn;
    public Transform EndPoint;
    public float flyDuration = 3f;

    Vector3 startPosition;
    SoundsManager SoundManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.PlayCrowCollide();
    }

    private void Awake()
    {
        startPosition = transform.position;
        SoundManager = FindObjectOfType<SoundsManager>();
    }

    public void Launch()
    {
        if (IsLaunch) return;
        currentTween?.Kill();
        IsLaunch = true;
        currentTween = transform.DOMoveX(EndPoint.position.x, flyDuration)
            .OnComplete(() =>
            {
                transform.position = startPosition;
                IsLaunch = false;
            });
    }
    Tween currentTween;
    public void Return()
    {
        currentTween?.Kill();
        IsReturn = true;
        mySprite.flipX = !mySprite.flipX;
        var allDistance = (EndPoint.position - startPosition).sqrMagnitude;
        var flyDistance = (startPosition - transform.position).sqrMagnitude;
        var returnTime = (flyDistance / allDistance) * flyDuration;
        currentTween = transform.DOMove(startPosition, returnTime).OnComplete(() => 
        { 
            IsLaunch = false;
            IsReturn = false;
            mySprite.flipX = !mySprite.flipX;
        });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsLaunch && !IsReturn)
        {
            SoundManager.PlayCrowFly();
            Return();
        }
    }
}
