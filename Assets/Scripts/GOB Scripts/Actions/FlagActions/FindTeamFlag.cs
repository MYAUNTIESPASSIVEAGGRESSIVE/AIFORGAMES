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
        // if the distance between the AI and the Friendly flag is short then collect the flag
        if (Vector3.Distance(_teamMember.transform.position, _teamMember._agentData.FriendlyFlag.transform.position) <= 1)
        {
            CollectAndReturn();
        }
        // if distance is high then find flag
        else FindFlag();
    }

    private bool FindFlag()
    {
        // if distance between AI and Flag is higher than 1 then the AI moves to the flag
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
        // collects the Flag
        _teamMember._agentActions.CollectItem(_teamMember._agentData.FriendlyFlag);
        _teamMember._agentInventory.AddItem(_teamMember._agentData.FriendlyFlag);

        // runs back to base
        _teamMember._agentActions.MoveTo(_teamMember._agentData.FriendlyBase);

        if(Vector3.Distance(_teamMember.transform.position, 
            _teamMember._agentData.FriendlyBase.transform.position) <= 1)
        {
            // when inside the base the flag is dropped
            _teamMember._agentActions.DropItem(_teamMember._agentData.FriendlyFlag);
            _teamMember._agentInventory.RemoveItem(_teamMember._agentData.FriendlyFlagName);

            // updating goals 1 (get flag) and 6 (keep flags at base)
            _teamMember.Gob_AI.UpdateGoals(5, _teamMember.FriendlyFlagDistance());
            _teamMember.Gob_AI.UpdateGoals(6, _teamMember.BothFlagDistance());

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
