using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FMODUnity;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    [field: Header("Stats")]
    [field: SerializeField]
    [field: Tooltip("The display name of this gun.")]
    public string gunName { get; private set; } = "basic";
    [field: SerializeField]
    [field: Tooltip("Whether the player is allowed to hold the button or needs to let go between shots.")]
    public bool automatic { get; private set; } = false;
    [field: Tooltip("Whether the pattern cycles through the shotPattern predictably or chooses randomly.")]
    public bool randomizePattern { get; private set; } = false;
    [field: SerializeField]
    [field: Tooltip("Reload time in seconds.")]
    public float reloadTime { get; private set; } = 2.0f;
    [field: SerializeField]
    [field: Tooltip("The amount of times this weapon can be shot before having to reload.")]
    public int maxAmmo { get; private set; } = 12;

    [field: SerializeField]
    [field: Tooltip("X: The maximum distance the weapon will stay at this damage amount.\nY: The damage the weapon will produce below this distance.")]
    public Vector2 closeDamage { get; private set; } = new Vector2(3, 30);

    [field: SerializeField]
    [field: Tooltip("X: The minimum distance the weapon will reach this damage amount.\nY: The damage the weapon will produce above this distance.")]
    public Vector2 farDamage { get; private set; } = new Vector2(15, 10);

    [field: SerializeField]
    [field: Tooltip("Data related to individual shots of this weapon. Used either cyclically or randomly depending on randomizePattern.")]
    public List<ShotShape> shotPattern { get; private set; } = new List<ShotShape>() { new ShotShape(Vector2.zero, 0.2f) };
    [field: SerializeField]
    [field: Tooltip("The distance bullets will check for collisions, measured in units.")]
    public float range { get; private set; } = 20.0f;
    [field: SerializeField]
    [field: Tooltip("The time it takes for the camera to start recentring after recoil. Measured in seconds.")]
    public float resetTime { get; private set; } = 0.3f;
    [field: SerializeField]
    [field: Tooltip("The maximum distance the camera can be offset by recoil in each axis. Measured in degrees.")]
    public Vector2 maxRecoilBounds { get; private set; } = Vector2.one * 20.0f;

    [field: Header("SFX")]
    [field: SerializeField]
    [field: Tooltip("The sound that plays when this weapon is used.")]
    public EventReference shootSound { get; private set; }
    [field: SerializeField]
    [field: Tooltip("The sound that plays when this weapon is reloaded.")]
    public EventReference reloadSound { get; private set; }
    [field: SerializeField]
    [field: Tooltip("The sound that plays when this weapon stops shooting.")]
    public EventReference stopShootSound { get; private set; }
}

[Serializable]
public class ShotShape
{
    [field: SerializeField]
    [field: Tooltip("The position of every bullet fired in this shot. Measured in degrees.")]
    public List<Vector2> points { get; private set; }
    [field: SerializeField]
    [field: Tooltip("The amount of time the weapon will be inactive after firing this shot. Measured in seconds.")]
    public float cooldownTime { get; private set; }
    [field: SerializeField]
    [field: Tooltip("The position that the camera will be moved to after firing this shot. Multiple recoils can add together. Both axis measured in degrees.")]
    public Vector2 recoilDirection { get; private set; }
    [field: SerializeField]
    [field: Tooltip("The amount of time it will take for the camera to move to the recoilDirection. Measured in seconds.")]
    public float recoilTime { get; private set; }

    public ShotShape(Vector2 point, float time)
    {
        points = new List<Vector2>();
        points.Add(point);
        cooldownTime = time;
        recoilDirection = Vector2.zero;
        recoilTime = 0.1f;
    }
}

