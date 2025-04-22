using UnityEngine;

public class FightEnemy : ActionBase
{
    public FightEnemy(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltaTime)
    {
        if (Vector3.Distance(_teamMember.transform.position, _teamMember._agentSenses.GetNearestEnemyInView().transform.position) <= 3.0f)
        {
            _teamMember._agentActions.MoveTo(_teamMember._agentSenses.GetNearestEnemyInView().transform.position);

            Fighting(_teamMember._agentSenses.GetNearestEnemyInView());
        }
    }

    public bool Fighting(GameObject Enemy)
    {
        _teamMember._agentActions.AttackEnemy(Enemy);

        if (!Enemy.gameObject.activeSelf)
        {
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
