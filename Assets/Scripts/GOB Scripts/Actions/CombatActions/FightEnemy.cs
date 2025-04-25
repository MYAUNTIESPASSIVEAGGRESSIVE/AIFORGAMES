using UnityEngine;

public class FightEnemy : ActionBase
{
    public FightEnemy(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltaTime)
    {
        if (Vector3.Distance(_teamMember.transform.position, _teamMember.TargetEnemy.transform.position) <= _teamMember._agentData.ViewRange &&
            !_teamMember._agentData.HasEnemyFlag)
        {
            _teamMember._agentActions.MoveTo(_teamMember.TargetEnemy.transform.position);

            Fighting(_teamMember.TargetEnemy);
        }
    }

    public bool Fighting(GameObject Enemy)
    {
        _teamMember._agentActions.AttackEnemy(Enemy);

        if (!Enemy.gameObject.activeSelf)
        {
            _teamMember.Gob_AI.UpdateGoals(4, 0);

            finished = true;
            return finished;
        }

        return finished;
    }

    public override string ToString()
    {
        return "Fighting Enemy";
    }
}
