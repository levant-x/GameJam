using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    public TMP_Text Text;
    public string Title = "Spawn Delay: ";
    public Slider SpeedSlider;

    private void Awake()
    {
        SpeedSlider.onValueChanged.AddListener(UpdateSpeedView);
    }

    private void UpdateSpeedView(float arg0)
    {
        var value =  Math.Round(SpeedSlider.value, 2);
        Text.text = Title + value;
    }
}
