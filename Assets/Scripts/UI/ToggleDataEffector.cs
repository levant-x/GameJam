using System;
using UnityEngine;
using UnityEngine.UI;

enum ToggleDataType
{
    Sprite, TextColor
}

[RequireComponent(typeof(Toggle))]
public class ToggleDataEffector : MonoBehaviour
{
    private Toggle toggle;
    private Text label;

    [SerializeField] private ToggleDataType dataType;

    [SerializeField] private Color colorOn;
    [SerializeField] private Color colorOff;

    [SerializeField] private Sprite spriteOn;
    [SerializeField] private Sprite spriteOff;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        if (dataType == ToggleDataType.TextColor) label = toggle.GetComponentInChildren<Text>();

        toggle.transition = Selectable.Transition.None;
        toggle.onValueChanged.AddListener(OnToggle);
        OnToggle(toggle.isOn);
    }

    void OnToggle(bool value)
    {
        if (dataType == ToggleDataType.Sprite) toggle.image.sprite = value ? spriteOn : spriteOff;
        if (dataType == ToggleDataType.TextColor) label.color = value ? colorOn : colorOff;
    }
}