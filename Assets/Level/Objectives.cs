using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Objectives
{
    [Serializable]
    public class Objective
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
            foreach (Requirement ii in requirements)
            {
                if (!ii.EvaluateRequirement(trackedStats[ii.statType])) output = false;
            }
            return output;
        }
    }

    [Serializable]
    public class Requirement
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
}
