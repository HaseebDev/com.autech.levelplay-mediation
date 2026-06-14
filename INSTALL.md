# Installing Autech LevelPlay Mediation

There are two supported ways to add this package to a Unity project.

## 1. Package Manager — git URL (recommended)

`Window → Package Manager → +  → Add package from git URL…` and paste:

```
https://github.com/HaseebDev/com.autech.levelplay-mediation.git
```

To pin a specific release, append a tag:

```
https://github.com/HaseebDev/com.autech.levelplay-mediation.git#v1.0.0
```

…or add it directly to `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.autech.levelplay-mediation": "https://github.com/HaseebDev/com.autech.levelplay-mediation.git#v1.0.0"
  }
}
```

## 2. `.unitypackage` from a GitHub Release

1. Open the [Releases page](https://github.com/HaseebDev/com.autech.levelplay-mediation/releases).
2. Download `com.autech.levelplay-mediation-<version>.unitypackage`.
3. In Unity: `Assets → Import Package → Custom Package…` and select the file.
   (Imports the package under `Assets/LevelPlay`.)

## Requirements

- Unity **2021.3** or newer.
- The **LevelPlay (Ads Mediation)** SDK — `com.unity.services.levelplay`
  (declared as a dependency; Package Manager resolves it automatically for
  method 1). Install it from the Unity Registry if you use method 2.

After importing, add the `VerifyandInitializeLevelPlay` prefab (Samples ▸ Prefabs)
to your first scene and fill in your LevelPlay app keys / ad unit ids. See the
[README](README.md) for the full quick-start.
