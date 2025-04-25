using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProtectFlagHolder : ActionBase
{
    public ProtectFlagHolder(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltatime)
    {
        GameObject NearestTeamMate = _teamMember._agentSenses.GetNearestFriendlyInView();

        if (TeamMateNear(NearestTeamMate))
        {
            ProtectTeamMate(NearestTeamMate);
        }
    }

    private bool TeamMateNear(GameObject nearTeamMate)
    {
        if (nearTeamMate.GetComponent<AgentData>().HasEnemyFlag)
        {
            return true;
        }
        return false;
    }

    private bool ProtectTeamMate(GameObject flagBarer)
    {
        if(flagBarer.GetComponent<AgentData>().HasEnemyFlag &&
            Vector3.Distance(flagBarer.transform.position, _teamMember.transform.position) <= 3)
        {
            _teamMember.Gob_AI.UpdateGoals(2, _teamMember.TeamMateHasFlag());

            finished = true;
            return finished;
        }

        _teamMember._agentActions.MoveTo(flagBarer.transform.position);

        finished = false;
        return finished;
    }

    public override string ToString()
    {
        return "Protecting Flag Holder";
    }
}
