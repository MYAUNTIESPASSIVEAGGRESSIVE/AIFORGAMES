using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTeamFlag : ActionBase
{
    public FindTeamFlag(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltatime)
    {
        if (Vector3.Distance(_teamMember.transform.position, _teamMember._agentData.FriendlyFlag.transform.position) <= 1)
        {
            CollectAndReturn();
        }
        else FindFlag();
    }

    private bool FindFlag()
    {
        if (Vector3.Distance(_teamMember.transform.position,
            _teamMember._agentData.FriendlyFlag.transform.position) > 1)
        {
            _teamMember._agentActions.MoveTo(_teamMember._agentData.FriendlyFlag.transform.position);
            return finished;
        }
        return finished;
    }

    private bool CollectAndReturn()
    {
        _teamMember._agentActions.CollectItem(_teamMember._agentData.FriendlyFlag);

        _teamMember._agentActions.MoveTo(_teamMember._agentData.FriendlyBase);

        if(Vector3.Distance(_teamMember.transform.position, 
            _teamMember._agentData.FriendlyBase.transform.position) <= 1)
        {
            _teamMember._agentActions.DropItem(_teamMember._agentData.FriendlyFlag);

            finished = true;
            return finished;
        }

        return finished;
    }

    public override string ToString()
    {
        return "Finding Team Flag";
    }
}
