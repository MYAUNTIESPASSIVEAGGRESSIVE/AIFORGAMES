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
            CollectFlag();
        }
    }

    public bool FindFlag()
    {
        if (Vector3.Distance(_teamMember.transform.position, _teamMember._agentData.EnemyFlag.transform.position) >= 1)
        {
            _teamMember._agentActions.MoveTo(_teamMember._agentData.EnemyFlag.transform.position);
            return false;
        }
        return true;
    }

    public bool CollectFlag()
    {
        _teamMember._agentActions.CollectItem(_teamMember._agentData.EnemyFlag);
        _teamMember.Gob_AI.UpdateGoals(4, _teamMember.GotEnemyFlag());

        finished = true;
        return finished;
    }

    public override string ToString()
    {
        return "Get Enemy Flag";
    }
}
