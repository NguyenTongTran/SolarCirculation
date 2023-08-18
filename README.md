# This project serve as submission for VRillAR test

## Implementation

- Plane is created to represent flat ground for shadow
- Capsule object with scaleY: 2 is used to represent human (1 unit = 1m)
- Directional Light is used to represent sun
- Simple input panel UI to allow user to change relevant config (longitude, altiude, month, day, time)
- Calculate altitude (rotationX in deg) and azimuth (rotationY in deg) based on relevant equations
- Update rotation of sun to reflect user config

## Assumption

- Use Y axis as reference for azimuth
- Use X axis as east/west (with right direction is east)
- Use Z axis as north/south (with forward direction is north)

## How to run

### Method 1

- Open project in Unity Editor
- Run with play mode
- Change values (longitude, altiude, month, day, time) on input panel
- Click update to view solar change

### Method 2

- Open on web browser: https://nguyentong.itch.io/solarcirculation

## Reference

- https://www.sciencedirect.com/topics/engineering/solar-declination
