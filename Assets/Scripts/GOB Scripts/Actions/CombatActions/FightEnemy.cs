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
        if(Vector3.Distance(_teamMember.transform.position, _teamMember._agentSenses.GetNearestEnemyInView().transform.position) < 0.50)
        {
            Fighting(_teamMember._agentSenses.GetNearestEnemyInView());
        }
    }

    public bool Fighting(GameObject Enemy)
    {
        _teamMember._agentActions.AttackEnemy(Enemy);

        if (Enemy = null)
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
