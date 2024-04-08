using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Objectives;

public class ObjectiveManager : MonoBehaviour
{
    Dictionary<string, float> trackedStats = new Dictionary<string, float>()
    {
        { "EnemiesKilled", 0 },
        { "EnemiesKilledInZone", 0 },
        { "TimeInZone", 0 },
        { "TimeSurvived", 0 },
        { "PlayersInZone", 0 }, //the amount of players in an area
        { "AllPlayersInZone", 0 }, //whether or not every player is in an area
        { "ButtonsPressed", 0 },
    };

    [SerializeField]
    private List<Objective> objectives;
    [SerializeField]
    private int objectiveIndex = 0;

    [SerializeField]
    HUDSystem hudRef;

    public void UpdateStat(string stat, float amount, bool checkForCompletion = true)
    {
        SetStat(stat, trackedStats[stat] + amount, checkForCompletion);
    }

    public void SetStat(string stat, float amount, bool checkForCompletion = true)
    {
        trackedStats[stat] = Mathf.Max(amount, 0);
        UpdatePlayerObjectivesHUD((int)trackedStats[stat]);
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
            UpdatePlayerObjectivesHUD();
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


    private void Start()
    {
        UpdatePlayerObjectivesHUD();
    }
    private void UpdatePlayerObjectivesHUD()
    {
        if (hudRef != null)
        {
            switch (objectives[objectiveIndex].requirements[0].statType)
            {
                case "EnemiesKilled":
                    hudRef.UpdateObjective((int)trackedStats["EnemiesKilled"], (int)objectives[objectiveIndex].requirements[0].minValue, "Current Objective:\nDefeat all Enemies");
                    break;
                default:
                    break;

            }
        }
    }

    public void ButtonObjectiveDisplay()
    {
        hudRef.NewButtonObjective();
    }

    public void NewObjectiveDisplay(string objectiveText)
    {
        hudRef.NewObjective(objectiveText);
    }

    private void UpdatePlayerObjectivesHUD(int amount)
    {
        if (hudRef != null) hudRef.UpdateObjective(amount);
    }
}
