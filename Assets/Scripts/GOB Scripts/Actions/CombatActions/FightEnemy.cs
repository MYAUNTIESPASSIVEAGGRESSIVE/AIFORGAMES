using UnityEngine;

public class FightEnemy : ActionBase
{
    public FightEnemy(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltaTime)
    {
        // checks if the target enemy is in range and the AI is not holding a flag
        if ((Vector3.Distance(_teamMember.transform.position, _teamMember.TargetEnemy.transform.position) <=
            _teamMember._agentData.ViewRange) &&
            (!_teamMember._agentData.HasEnemyFlag || !_teamMember._agentData.HasFriendlyFlag))
        {
            // moves to the target enemy and attacks
            _teamMember._agentActions.MoveTo(_teamMember.TargetEnemy.transform.position);

            Fighting(_teamMember.TargetEnemy);
        }
    }

    public bool Fighting(GameObject Enemy)
    {
        // attacks enemy
        _teamMember._agentActions.AttackEnemy(Enemy);

        // if the enemy is dead/null then the goal updates and is finished
        if (Enemy == null)
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
