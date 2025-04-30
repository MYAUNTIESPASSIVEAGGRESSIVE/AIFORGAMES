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
            CollectFlag();
        }
        // if distance is high then find flag
        else FindFlag();
    }

    private bool FindFlag()
    {
        Vector3 range = _teamMember._agentData.FriendlyFlag.transform.position - _teamMember.transform.position;
        float distance = range.sqrMagnitude;

        if (distance < 2)
        {
            return true;
        }

        if (_teamMember._agentData.HasFriendlyFlag)
        {
            return true;
        }

        _teamMember._agentActions.MoveTo(_teamMember._agentData.FriendlyFlag.transform.position);

        return false;
    }

    private bool CollectFlag()
    {
        // when in range of the flag they collect it.
        _teamMember._agentActions.CollectItem(_teamMember._agentData.FriendlyFlag);
        _teamMember._agentInventory.AddItem(_teamMember._agentData.FriendlyFlag);

        // updates appropriate goals
        _teamMember.Gob_AI.UpdateGoals(1, _teamMember.GotFlag());

        if (_teamMember._agentData.HasFriendlyFlag)
        {
            ReturnFlag();
        }

        return finished;
    }

    private bool ReturnFlag()
    {
        Vector3 range = _teamMember._agentData.FriendlyBase.transform.position - _teamMember.transform.position;
        float distance = range.sqrMagnitude;

        if (distance < 5)
        {
            // if in base then drop the flag
            _teamMember._agentActions.DropItem(_teamMember._agentData.FriendlyFlag);
            _teamMember._agentInventory.RemoveItem(_teamMember._agentData.FriendlyFlagName);

            // updating goals 1 (get flag) and 6 (keep flags at base)
            _teamMember.Gob_AI.UpdateGoals(1, _teamMember.GotFlag());
            _teamMember.Gob_AI.UpdateGoals(6, _teamMember.BothFlagDistance());
            _teamMember.Gob_AI.UpdateGoals(5, _teamMember.FriendlyFlagDistance());

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
