using System.Collections.Generic;
using UnityEngine;

public class ProtectFlagHolder : ActionBase
{
    public ProtectFlagHolder(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltatime)
    {
        List<GameObject> teamMates = _teamMember._agentSenses.GetFriendliesInView();
    
        foreach (GameObject teammate in teamMates)
        {
            if(teammate.GetComponent<AgentData>().HasEnemyFlag | teammate.GetComponent<AgentData>().HasFriendlyFlag)
            {
                ProtectTeamMate(teammate);
            }
        }
    }

    private bool ProtectTeamMate(GameObject flagBarer)
    {
        Vector3 range = flagBarer.transform.position - _teamMember.transform.position;
        float distance = range.sqrMagnitude;

        // checks the distance is shorter than 3 between AI and flag barer
        if (distance <= 10)
        {
            // updates the goal with the distance value x10
            _teamMember.Gob_AI.UpdateGoals(2, 10 * distance);

            finished = true;
            return finished;
        }

        // if distance is long then move to flagbarer
        _teamMember._agentActions.MoveTo(flagBarer.transform.position);
        return finished;
    }

    public override string ToString()
    {
        return "Protecting Flag Holder";
    }
}
