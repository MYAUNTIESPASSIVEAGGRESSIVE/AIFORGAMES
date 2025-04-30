using UnityEngine;

public class GetEnemyFlag : ActionBase
{
    public GetEnemyFlag(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltaTime)
    {
        _teamMember._agentActions.MoveTo(_teamMember._agentData.EnemyFlag.transform.position);

        // if the AI is that the base and at the flag then they collect the flag
        if (FindFlag())
        {
            CollectFlag();
        }
    }

    public bool FindFlag()
    {
        Vector3 range = _teamMember._agentData.EnemyFlag.transform.position - _teamMember.transform.position;
        float distance = range.sqrMagnitude;

        if (distance < 2)
        {
            return true;
        }

        if (_teamMember._agentData.HasEnemyFlag)
        {
            return true;
        }

        _teamMember._agentActions.MoveTo(_teamMember._agentData.EnemyFlag.transform.position);

        return false;
    }

    public bool CollectFlag()
    {
        // when in range of the flag they collect it.
        _teamMember._agentActions.CollectItem(_teamMember._agentData.EnemyFlag);
        _teamMember._agentInventory.AddItem(_teamMember._agentData.EnemyFlag);

        // updates appropriate goals
        _teamMember.Gob_AI.UpdateGoals(1, _teamMember.GotFlag());

        if (_teamMember._agentData.HasEnemyFlag)
        {
            GetToBase();
        }

        return finished;
    }

    private bool GetToBase()
    {
        // move to the friendly base
        _teamMember._agentActions.MoveTo(_teamMember._agentData.FriendlyBase);

        Vector3 range = _teamMember._agentData.FriendlyBase.transform.position - _teamMember.transform.position;
        float distance = range.sqrMagnitude;

        if (distance < 5)
        {
            // if in base then drop the flag
            _teamMember._agentActions.DropItem(_teamMember._agentData.EnemyFlag);
            _teamMember._agentInventory.RemoveItem(_teamMember._agentData.EnemyFlagName);

            _teamMember.Gob_AI.UpdateGoals(6, _teamMember.BothFlagDistance());

            finished = true;
            return finished;
        }

        return finished;
    }


    public override string ToString()
    {
        return "Get Enemy Flag";
    }
}
