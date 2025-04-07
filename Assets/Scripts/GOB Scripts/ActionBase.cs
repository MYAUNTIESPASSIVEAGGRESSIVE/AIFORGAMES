using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBase
{
    // calls AI.
    protected AI TeamMember;

    // dictionary of goals of which the action satisfies.
    protected Dictionary<int, float> _goalsatified;

    // tracking time and completion of action.
    protected float timer = 0.0f;

    protected bool finished;

    public ActionBase(AI teamMember)
    {
        _goalsatified = new Dictionary<int, float>();
        TeamMember = teamMember;
        timer = 0.0f;
        finished = false;
    }

    //bool to check if action is complete
    public bool IsFinished
    {
        get { return finished; }
        set { finished = value; }
    }

    public float EvaluateGoalSatisfaction(int ID)
    {
        if(_goalsatified.ContainsKey(ID))
        {
            return _goalsatified[ID];
        }
        
        return 0.0f;
    }

    public void SetGoalSatifiaction(int ID, float value)
    {
        if (_goalsatified.ContainsKey(ID))
        {
            _goalsatified[ID] = value;
        }
        else
        {
            _goalsatified.Add(ID, value);
        }
    }

    // executes the action
    public abstract void Execute(float deltatime);

    // get action name (for debugging)
    public override abstract string ToString();
}
