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
        _teamMember._agentActions.MoveTo(_teamMember._agentData.FriendlyBase);

        if(Vector3.Distance(_teamMember.transform.position, _teamMember._agentData.FriendlyBase.transform.position) <= 1)
        {
            _teamMember._agentActions.DropItem(_teamMember._agentData.EnemyFlag);

            finished = true;
            return finished;
        }

        return finished;
    }

    public override string ToString()
    {
        return "Returning Enemy Flag";
    }
}
