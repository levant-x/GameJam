using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class RotateToggle : MonoBehaviour
{
    Toggle toggle;
    public float RotateDuration = 0.5f;
    public Transform RotateObject;
    public GameObject OffList;
    Tween currentTween;
    private void Awake() 
    { 
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnChange);
    }

    private void OnChange(bool arg0)
    {
        currentTween?.Kill();
        currentTween = RotateObject.DORotate(arg0 ? Vector3.zero : Vector3.forward*180f, RotateDuration);
        OffList.SetActive(arg0);
    }
}
