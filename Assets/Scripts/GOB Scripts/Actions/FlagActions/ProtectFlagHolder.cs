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
        if(flagBarer.GetComponent<AgentData>().HasEnemyFlag 
            && flagBarer.transform.position 
            == _teamMember._agentData.FriendlyBase.transform.position)
        {
            finished = true;
            return finished;
        }

        finished = false;
        return finished;
    }

    public override string ToString()
    {
        return "Protecting Flag Holder";
    }
}
