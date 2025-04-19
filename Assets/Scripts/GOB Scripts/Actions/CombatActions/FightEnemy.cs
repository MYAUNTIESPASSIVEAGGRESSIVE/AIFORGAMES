using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEnemy : ActionBase
{
    public FightEnemy(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltaTime)
    {
        if(Vector3.Distance(_teamMember.transform.position, _teamMember._agentSenses.GetNearestEnemyInView().transform.position) <= 1)
        {
            Fighting(_teamMember._agentSenses.GetNearestEnemyInView());
        }
    }

    public bool Fighting(GameObject Enemy)
    {
        _teamMember._agentActions.AttackEnemy(Enemy);

        if (Enemy = null)
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
