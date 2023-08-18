using System;
using UnityEngine;

public struct SolarAngle
{
    public float Altitude;
    public float Azimuth;
}

public class SolarManager : MonoBehaviour
{
    [SerializeField] private Transform _solar;
    [SerializeField] private float _latitude;
    [SerializeField] private float _longtitude;
    [SerializeField] private int _month = 1;
    [SerializeField] private int _day = 1;
    [SerializeField] private float _time;

    public static SolarManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void OnValidate()
    {
        UpdateSunRotation_Internal();
    }

    public void UpdateSunRotation(float latitude, float longitude, int month, int day, float time)
    {
        _latitude = latitude;
        _longtitude = longitude;
        _month = month;
        _day = day;
        _time = time;

        UpdateSunRotation_Internal();
    }

    private void UpdateSunRotation_Internal()
    {
        float solarDeclination = CalculateSolarDeclinationInRad(_month, _day);
        float hourAngle = CalculateHourAngleInRad(_longtitude, _time);

        // Calculate the solar angles
        float latRad = Mathf.Deg2Rad * _latitude;
        SolarAngle solarAngle = CalculateSolarAnglesInRad(latRad, solarDeclination, hourAngle);
        
        // Apply rotation to the solar
        Vector3 sunRotation = new Vector3(solarAngle.Altitude * Mathf.Rad2Deg, solarAngle.Azimuth * Mathf.Rad2Deg, 0.0f);
        _solar.rotation = Quaternion.Euler(sunRotation);
    }
    
    private float CalculateSolarDeclinationInRad(int month, int day)
    {
        DateTime date = new DateTime(2023, month, day);
        float declination = 23.45f * Mathf.Sin(360f * (284 + date.DayOfYear) / 365f);
        return Mathf.Deg2Rad * declination;
    }
    
    private float CalculateSolarNoon(float longitude)
    {
        return 12f - (longitude / 15f);
    }
    
    private float CalculateHourAngleInRad(float longitude, float time)
    {
        float solarNoon = CalculateSolarNoon(longitude);
        float hourAngle = (solarNoon - time) * 15f;
        return Mathf.Deg2Rad * hourAngle;
    }

    private SolarAngle CalculateSolarAnglesInRad(float latitude, float solarDeclination, float hourAngle)
    {
        float sinAltitude = Mathf.Sin(latitude) * Mathf.Sin(solarDeclination) + Mathf.Cos(latitude) * Mathf.Cos(solarDeclination) * Mathf.Cos(hourAngle);
        float altitude = Mathf.Asin(sinAltitude);
        
        float cosAzimuth = Mathf.Clamp((Mathf.Sin(latitude) * sinAltitude - Mathf.Sin(solarDeclination)) / Mathf.Cos(latitude) * Mathf.Cos(altitude), -1f, 1f);
        float azimuth = Mathf.Acos(cosAzimuth);

        return new SolarAngle { Altitude = altitude, Azimuth = azimuth };
    }
}
