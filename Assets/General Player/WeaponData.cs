using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    [field: Header("Stats")]
    [field: SerializeField]
    public string gunName { get; private set; } = "basic";
    [field: SerializeField]
    [Tooltip("Whether the player is allowed to hold the button or needs to let go between shots.")]
    public bool automatic { get; private set; } = false;
    [Tooltip("Whether the pattern cycles through the shotPattern predictably or chooses randomly.")]
    public bool randomizePattern { get; private set; } = false;
    [field: SerializeField]
    [Tooltip("Reload time in seconds.")]
    public float reloadTime { get; private set; } = 2.0f;
    [field: SerializeField]
    public int maxAmmo { get; private set; } = 12;

    [field: SerializeField]
    [Tooltip("X: The maximum distance the weapon will stay at this damage amount.\nY: The damage the weapon will produce below this distance.")]
    public Vector2 closeDamage { get; private set; } = new Vector2(3, 30);

    [field: SerializeField]
    [Tooltip("X: The minimum distance the weapon will reach this damage amount.\nY: The damage the weapon will produce above this distance.")]
    public Vector2 farDamage { get; private set; } = new Vector2(15, 10);

    [field: SerializeField]
    public List<ShotShape> shotPattern { get; private set; } = new List<ShotShape>() { new ShotShape(Vector2.zero, 0.2f) };
    [field: SerializeField]
    public float range { get; private set; } = 20.0f;
    [field: SerializeField]
    public float resetTime { get; private set; } = 0.3f;
}

[Serializable]
public class ShotShape
{
    [field: SerializeField]
    public List<Vector2> points { get; private set; }
    [field: SerializeField]
    public float cooldownTime { get; private set; }
    [field: SerializeField]
    public Vector2 recoilDirection { get; private set; }
    [field: SerializeField]
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

