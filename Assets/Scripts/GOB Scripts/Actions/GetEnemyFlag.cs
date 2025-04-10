using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetEnemyFlag : ActionBase
{

    public GetEnemyFlag(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltaTime)
    {
        if (FindBase() && FindFlag())
        {
            CollectFlag(deltaTime);
        }
    }

    private bool FindBase()
    {
        if (!(TeamMember.transform.position == TeamMember.TeamBlackboard.GetVector3("EnemyBase")))
        {
            TeamMember._agentActions.MoveTo(TeamMember.TeamBlackboard.GetVector3("EnemyBase"));
            return false;
        }
        return true;
    }

    public bool FindFlag()
    {
        if(!(TeamMember.transform.position == 
            TeamMember.TeamBlackboard.GetGameObject("EnemyFlag").transform.position))
        {
            TeamMember._agentActions.MoveTo(TeamMember.TeamBlackboard.GetGameObject("EnemyFlag").transform.position);
            return false;
        }
        return true;
    }

    private void CollectFlag(float deltaTime)
    {
        TeamMember._agentInventory.AddItem(TeamMember.TeamBlackboard.GetGameObject("EnemyFlag"));
        TeamMember.Gob_AI.UpdateGoals(2, TeamMember.GotEnemyFlag());
    }

    public override string ToString()
    {
        return "Get Enemy Flag";
    }
}
