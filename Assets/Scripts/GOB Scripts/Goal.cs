using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class CurveFunctions
{
    private static float _lowerRange = 0.0f;
    private static float _upperRange = 1.0f;

    // normalised half step curve function
    public static float StepAtUpper(float lowerRangeVal, float upperRangeVal, float Value)
    {
        if(Value >= upperRangeVal)
        {
            return _upperRange;
        }

        return _lowerRange;
    }

    // normalised linear curve function
    public static float Linear(float lowerRangeVal, float upperRangeVal, float Value)
    {
        return _lowerRange + (_upperRange - _lowerRange) * (Value - lowerRangeVal) / (upperRangeVal - lowerRangeVal);
    }

    // normalised exponential curve function
    public static float Exponential(float lowerRangeVal, float upperRangeVal, float Value)
    {
        const float power = 3.5f;

        return _lowerRange + (_upperRange - _lowerRange) * Mathf.Pow((Value - lowerRangeVal) / (upperRangeVal - lowerRangeVal), power);
    }

    // normalised logarithmic curve function
    public static float Logarithmic(float lowerRangeVal, float upperRangeVal, float Value)
    {
        const float power = 0.2f;
        return _lowerRange + (_upperRange - _lowerRange) * Mathf.Pow((Value - lowerRangeVal) / (upperRangeVal - lowerRangeVal), power);
    }

    // normalised linear curve function
    public static float ReverseLinear(float lowerRangeVal, float upperRangeVal, float Value)
    {
        return 1 - _lowerRange + (_upperRange - _lowerRange) * (Value - lowerRangeVal) / (lowerRangeVal - upperRangeVal);
    }
}

// Delegate for the curve functions
public delegate float CurveFunction(float lowerRangeVal, float upperRangeVal, float value);

// goal logic storing the value of the goal and curve function.
public class GoalBase
{
    public int GoalIndex;

    private float _LowerRange;
    private float _UpperRange;

    private float _Value;

    CurveFunction _curveFunction;

    //Takes the values from the SO and puts them into the GoalBase Class.
    public GoalBase(float val, SO_Goals GoalSO, CurveFunction GoalCurve)
    {
        GoalIndex = GoalSO.GoalIndex;
        _LowerRange = GoalSO.GoalBaseValue;
        _UpperRange = GoalSO.GoalFinalValue;

        _Value = val;

        _curveFunction = GoalCurve;
    }

    public float BaseValue
    {
        get { return _Value; }
        set { _Value = value; }
    }

    public float FinalValue
    {
        get { return _curveFunction.Invoke(_LowerRange, _UpperRange, _Value); }
    }
}
