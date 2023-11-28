using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;

public class ObjectiveManager : MonoBehaviour
{
    Dictionary<string, float> trackedStats = new Dictionary<string, float>()
    {
        { "EnemiesKilled", 0 },
        { "TimeInZone", 0 },
        { "TimeSurvived", 0 },
        { "PlayersInZone", 0 }, //the amount of players in an area
        { "AllPlayersInZone", 0 } //whether or not every player is in an area
    };

    [SerializeField]
    private List<Objective> objectives;
    [SerializeField]
    private int objectiveIndex = 0;

    [Serializable] public class Objective
    {
        [field: SerializeField] public List<Requirement> requirements { get; private set; }
        [field: SerializeField] public UnityEvent completionEvent { get; private set; }

        public Objective()
        {
            requirements = new List<Requirement>();
            completionEvent = new UnityEvent();
        }

        public bool EvaluateObjective(Dictionary<string, float> trackedStats)
        {
            bool output = true;
            foreach(Requirement ii in requirements)
            {
                if (!ii.EvaluateRequirement(trackedStats[ii.statType])) output = false;
            }
            return output;
        }
    }

    [Serializable] public class Requirement
    {
        [field: SerializeField] public string statType { get; private set; }
        [field: SerializeField] public float minValue { get; private set; }
        [field: SerializeField] public float maxValue { get; private set; }

        public Requirement(float initMinValue)
        {
            statType = "EnemiesKilled";
            minValue = initMinValue;
            maxValue = 0;
        }

        public bool EvaluateRequirement(float value)
        {
            if (value < minValue && minValue != 0) return false;
            else if (value > maxValue && maxValue != 0) return false;
            else return true;
        }
    }

    public void UpdateStat(string stat, float amount, bool checkForCompletion)
    {
        trackedStats[stat] += amount;

        if (!checkForCompletion) return;
        if (objectives[objectiveIndex].EvaluateObjective(trackedStats)) CompleteObjective();
    }

    private void CompleteObjective()
    {
        objectives[objectiveIndex].completionEvent.Invoke();
        ResetTrackedStats();

        if (objectives.Count > objectiveIndex + 1)
        {
            objectiveIndex += 1;
        }
        else
        {
            print("Win!");
        }
    }

    private void ResetTrackedStats()
    {
        foreach(string stat in trackedStats.Keys.ToList())
        {
            trackedStats[stat] = 0;
        }
    }
}
