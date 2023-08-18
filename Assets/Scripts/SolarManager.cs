using System;
using UnityEngine;

public struct SolarAngle
{
    public float Altitude;
    public float Azimuth;
}

public class SolarManager : MonoBehaviour
{
    public Transform Solar; // Reference to your Directional Light
    public float Latitude = 0.0f; // Latitude in degrees
    public float Longitude = 0.0f; // Longitude in degrees
    public int Month = 1; // Month (1-12)
    public int Day = 1; // Day (1-31)
    public float Time = 12.0f;

    void OnValidate()
    {
        UpdateSunRotation();
    }

    private void UpdateSunRotation()
    {
        float solarDeclination = CalculateSolarDeclinationInRad(Month, Day);
        float hourAngle = CalculateHourAngleInRad(Longitude, Time);

        // Calculate the solar angles
        float latRad = Mathf.Deg2Rad * Latitude;
        SolarAngle solarAngle = CalculateSolarAnglesInRad(latRad, solarDeclination, hourAngle);
        
        // Apply rotation to the solar
        Vector3 sunRotation = new Vector3(solarAngle.Altitude * Mathf.Rad2Deg, solarAngle.Altitude * Mathf.Rad2Deg, 0.0f);
        Solar.rotation = Quaternion.Euler(sunRotation);
    }
    
    private float CalculateSolarDeclinationInRad(int month, int day)
    {
        DateTime dt = new DateTime(2023, month, day);
        float declination = 23.45f * Mathf.Sin(360f * (284 + dt.DayOfYear) / 365f);
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
        
        float cosSAA = Mathf.Clamp((Mathf.Sin(latitude) * sinAltitude - Mathf.Sin(solarDeclination)) / Mathf.Cos(latitude) * Mathf.Cos(altitude), -1f, 1f);
        float azimuth = Mathf.Acos(cosSAA);

        return new SolarAngle { Altitude = altitude, Azimuth = azimuth };
    }
}
