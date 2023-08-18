# This project serve as submission for VRillAR test

## Implementation

- Plane is created to represent flat ground for shadow
- Capsule object with scaleY: 2 is used to represent human (1 unit = 1m)
- Directional Light is used to represent sun
- Simple input panel UI to allow user to change relevant config (longitude, altiude, month, day, time)
- Calculate altitude (rotationX in deg) and azimuth (rotationY in deg) based on relevant equations
- Update rotation of sun to reflect user config

## How to run

- Open project in Unity Editor
- Run with play mode
- Change values (longitude, altiude, month, day, time) on input panel
- Click update to view solar change

## Reference

- https://www.sciencedirect.com/topics/engineering/solar-declination
