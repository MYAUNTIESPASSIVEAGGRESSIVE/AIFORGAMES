using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReturnEnemyFlag : ActionBase
{
    public ReturnEnemyFlag(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltatime)
    {
        if (_teamMember._agentData.HasEnemyFlag)
        {
            GetToBase();
        }
    }

    private bool GetToBase()
    {
        _teamMember._agentActions.MoveTo(_teamMember._agentData.EnemyBase);

        if(_teamMember.transform.position == 
            _teamMember._agentData.FriendlyBase.transform.position)
        {
            _teamMember._agentActions.DropItem(_teamMember._agentData.EnemyFlag);
        }

        finished = true;
        return finished;
    }

    public override string ToString()
    {
        return "Returning Enemy Flag";
    }
}
