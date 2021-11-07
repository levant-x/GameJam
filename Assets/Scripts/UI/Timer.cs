using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public event Action OnTimeElapsed;

    [SerializeField] private Text timerText;
    [SerializeField] private Image indicator;

    private float _secondsLeft = 0;
    private float _initialValue = 0;

    public void SetTime(float seconds)
    {
        _secondsLeft = _initialValue = seconds;
    }

    private void Update()
    {
        if (_secondsLeft == 0) return;
        _secondsLeft -= Time.deltaTime;
        UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        float minutes = Mathf.FloorToInt(_secondsLeft / 60);
        float seconds = Mathf.FloorToInt(_secondsLeft % 60);
        var fraction = _secondsLeft / _initialValue;

        timerText.text = $"{minutes}:{seconds}";
        indicator.fillAmount = fraction;

        if (_secondsLeft < 0)
        {
            _secondsLeft = 0;
            OnTimeElapsed?.Invoke();
        }
    }
}