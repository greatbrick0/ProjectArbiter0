using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWrangler : MonoBehaviour
{
    [SerializeField]
    PlayerTracker tracker;

    [SerializeField]
    [Tooltip("The distance that a player must be away from the point to be teleported.")]
    private float distance = 20.0f;
    [SerializeField]
    [Tooltip("The point players will be teleported to.")]
    Transform point;

    public void Wrangle()
    {
        tracker.WrangleAllPlayers(point.position, distance);
    }
}
