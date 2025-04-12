using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_AI
{
    // goal list
    private List<GoalBase> Goals = new List<GoalBase>();

    //action list
    private List<ActionBase> Actions = new List<ActionBase>();

    // adds a goal to the goal list
    public void AddGoal(GoalBase goal)
    {
        Goals.Add(goal);
    }

    // adds an action to action list
    public void AddAction(ActionBase action)
    {
        Actions.Add(action);
    }

    // updates the appropriate goals value
    // this is done when a goal insistance is change or when an action is complete.
    public void UpdateGoals(int ID, float value)
    {
        foreach (GoalBase goal in Goals)
        {
            if (goal.GoalIndex == ID)
            {
                goal.BaseValue = value;
            }
        }
    }

    //  evaluates the goal with the highest insistance and then chooses the action with most utility
    public ActionBase ChooseAction(AI teamMember)
    {
        GoalBase highestGoal = Goals[0];
        // checks each goal within the list
        foreach (GoalBase goals in Goals)
        {
            // gets the goal with highest insistance value
            if(goals.FinalValue > highestGoal.FinalValue)
            {
                highestGoal = goals;
            }
        }

        ActionBase highestAction = Actions[0];
        // checks each action within the list
        foreach (ActionBase action in Actions)
        {
            // chooses the highest utilising action for the goal
            if (action.EvaluateGoalSatisfaction(highestGoal.GoalIndex) > highestAction.EvaluateGoalSatisfaction(highestGoal.GoalIndex))
            {
                highestAction = action;
            }
        }
        return highestAction;
    }
}
