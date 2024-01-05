using System;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Text _sliderValue;
    [SerializeField] private Slider _slider;
    
    public void LimitFrameRate(float limiter)
    {
        Application.targetFrameRate = (int)limiter;
    }

    private void Update()
    {
        _sliderValue.text = _slider.value.ToString();
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        _slider.value = Application.targetFrameRate;
    }
}
