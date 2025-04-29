using UnityEngine;

public class GetEnemyFlag : ActionBase
{
    public GetEnemyFlag(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltaTime)
    {
        // if the AI is that the base and at the flag then they collect the flag
        if (FindFlag())
        {
            CollectFlag(deltaTime);
        }
    }

    public bool FindFlag()
    {
        // if the flag is out of range of the AI and is not inside the base then they go to the flag
        if (Vector3.Distance(_teamMember.transform.position, _teamMember._agentData.EnemyFlag.transform.position) >= 1 &&
            Vector3.Distance(_teamMember._agentData.EnemyFlag.transform.position, _teamMember._agentData.FriendlyBase.transform.position) >= 6)
        {
            _teamMember._agentActions.MoveTo(_teamMember._agentData.EnemyFlag.transform.position);
            return false;
        }
        return true;
    }

    public bool CollectFlag(float deltaTime)
    {
        // when in range of the flag they collect it.
        _teamMember._agentActions.CollectItem(_teamMember._agentData.EnemyFlag);
        _teamMember._agentInventory.AddItem(_teamMember._agentData.EnemyFlag);

        // updates appropriate goals
        _teamMember.Gob_AI.UpdateGoals(1, _teamMember.GotFlag());

        if (_teamMember._agentData.HasEnemyFlag)
        {
            GetToBase(deltaTime);
        }

        return finished;
    }

    private bool GetToBase(float deltaTime)
    {
        // move to the friendly base
        _teamMember._agentActions.MoveTo(_teamMember._agentData.FriendlyBase);

        if (Vector3.Distance(_teamMember.transform.position, _teamMember._agentData.FriendlyBase.transform.position) <= 5)
        {
            // if in base then drop the flag
            _teamMember._agentActions.DropItem(_teamMember._agentData.EnemyFlag);
            _teamMember._agentInventory.RemoveItem(_teamMember._agentData.EnemyFlagName);

            // updating goals 1 (get flag) and 6 (keep flags at base)
            _teamMember.Gob_AI.UpdateGoals(1, _teamMember.GotFlag());
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
