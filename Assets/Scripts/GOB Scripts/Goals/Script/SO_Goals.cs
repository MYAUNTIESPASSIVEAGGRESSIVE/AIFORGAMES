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

    [Header("Goal Information")]
    public int GoalIndex;
    public string GoalName;

    [Header("Goal Insistance")]
    public float GoalBaseValue;
    public float GoalFinalValue;

    [Tooltip("Used For classification rather than functionaility")]
    public GoalCurve GoalCurveFunction;


}
