using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CloudClick : MonoBehaviour, IPointerDownHandler
{
    public static event Action<Collider2D> OnClickCloud;
    Collider2D col;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnClickCloud?.Invoke(col);
    }
}
