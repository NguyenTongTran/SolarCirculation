using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _longitude;
    [SerializeField] private TMP_InputField _latitude;
    [SerializeField] private TMP_InputField _month;
    [SerializeField] private TMP_InputField _day;
    [SerializeField] private TMP_InputField _time;
    [SerializeField] private Button _updateButton;

    void Awake()
    {
        _longitude.onValueChanged.AddListener(OnLongitudeChanged);
        _latitude.onValueChanged.AddListener(OnLatitudeChanged);
        _month.onValueChanged.AddListener(OnMonthChanged);
        _day.onValueChanged.AddListener(OnDayChanged);
        _time.onValueChanged.AddListener(OnTimeChanged);
        _updateButton.onClick.AddListener(OnUpdateButtonClicked);
    }

    private void OnUpdateButtonClicked()
    {
        var longitude = float.Parse(_longitude.text);
        var latitude = float.Parse(_latitude.text);
        var month = int.Parse(_month.text);
        var day = int.Parse(_day.text);
        var time = float.Parse(_time.text);

        SolarManager.Instance.UpdateSunRotation(latitude, longitude, month, day, time);
    }

    private void OnLongitudeChanged(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            _longitude.text = "0";
            return;
        }

        var result = float.Parse(value);
        if (result >= -180f && result <= 180f) return;
        
        _longitude.text = Mathf.Clamp(result, -180f, 180f).ToString();
    }
    
    private void OnLatitudeChanged(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            _latitude.text = "0";
            return;
        }
        
        var result = float.Parse(value);
        if (result >= -90f && result <= 90f) return;
        
        _latitude.text = Mathf.Clamp(result, -90f, 90f).ToString();
    }
    
    private void OnMonthChanged(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            _month.text = "1";
            OnDayChanged(_day.text);
            return;
        }

        var result = int.Parse(value);
        if (result >= 1 && result <= 12)
        {
            OnDayChanged(_day.text);
            return;
        }
        
        _month.text = Mathf.Clamp(result, 1, 12).ToString();
        OnDayChanged(_day.text);
    }
    
    private void OnDayChanged(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            _day.text = "1";
            return;
        }
        
        var result = int.Parse(value);
        var month = int.Parse(_month.text);
        var maxDays = DateTime.DaysInMonth(ConfigConstants.CurrentYear, month);
        
        if (result >= 1 && result <= maxDays) return;
        
        _day.text = Mathf.Clamp(result, 1, maxDays).ToString();
    }
    
    private void OnTimeChanged(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            _time.text = "0";
            return;
        }
        
        var result = float.Parse(value);
        if (result >= 0f && result <= 24f) return;
        
        _time.text = Mathf.Clamp(result, 0f, 24f).ToString();
    }
}
