using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoalSO", menuName = "ScriptableObjects/GoalSO", order = 1)]
public class SO_Goals : ScriptableObject
{
    public enum GoalCurve
    {
        Step,
        Linear,
        Exponential,
        Logarithmic,
        ReverseLinear,
    }

    [Header("Goal Infomation")]
    public int GoalIndex;
    public string GoalName;

    [Header("Goal Insistance")]
    public float GoalBaseValue;
    public float GoalFinalValue;
    public GoalCurve GoalCurveFunction;


}
