
/*
    MAKE SURE TO DISABLE THIS ZONE ONCE THE OBJECTIVE IS COMPLETE!
    if you do not do so, the players could keep standing in this zone to gain progress in the next objective.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CaptureZone : MonoBehaviour
{
    private ObjectiveManager objectiveTracker;
    private PlayerTracker playerTracker;
    private int playersInZone = 0;
    [SerializeField]
    private bool trackTime = true;
    [SerializeField]
    private bool trackPlayerCount = false;
    [SerializeField]
    private bool trackAllPlayersCondition = false;
    [SerializeField]
    [Tooltip("The amount the score will increase when players are in the zone. " +
        "\nThe 0th rate will be used when one player is in, the 1st rate will be used when two players are in, etc. " +
        "\nIf more players are in the zone than there are rates in the list, the last rate will be used. ")]
    private List<float> ratePerPlayer = new List<float>() { 1.0f };

    private void Start()
    {
        objectiveTracker = FindObjectOfType<ObjectiveManager>();
        if (trackAllPlayersCondition) playerTracker = FindObjectOfType<PlayerTracker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playersInZone += 1;
            print("in");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersInZone -= 1;
            print("out");
        }
    }

    private void Update()
    {
        if (trackTime)
        {
            if (playersInZone > 0)
            {
                if (playersInZone > ratePerPlayer.Count) UpdateTracker(ratePerPlayer[^1]);
                else UpdateTracker(ratePerPlayer[playersInZone - 1]);
            }
        }
        if (trackPlayerCount)
        {
            SetTracker(playersInZone, "PlayersInZone");
        }
        if (trackAllPlayersCondition)
        {
            SetTracker((playersInZone == playerTracker.playerCount) ? 1 : 0, "AllPlayersInZone");
        }
    }

    private void UpdateTracker(float amount, string tracker = "TimeInZone")
    {
        if (tracker == "TimeInZone") amount *= Time.deltaTime;
        objectiveTracker.UpdateStat(tracker, amount);
    }

    private void SetTracker(float amount, string tracker)
    {
        objectiveTracker.SetStat(tracker, amount);
    }
}
