using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoalSO", menuName = "ScriptableObjects/GoalSO", order = 1)]
public class SO_Goals : ScriptableObject
{
    // holds information for debugging and indexing the Goals
    [Header("Goal Information")]
    public int GoalIndex;
    public string GoalName;

    // holds the base and final value of the goal
    [Header("Goal Insistance")]
    public float GoalBaseValue;
    public float GoalFinalValue;


}
