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
        if(Vector3.Distance(_teamMember.transform.position, _teamMember._agentSenses.GetNearestEnemyInView().transform.position) <= 2)
        {
            _teamMember._agentActions.MoveTo(_teamMember._agentSenses.GetNearestEnemyInView().transform.position);

            Fighting(_teamMember._agentSenses.GetNearestEnemyInView());
        }
    }

    public bool Fighting(GameObject Enemy)
    {
        Debug.Log("Attacking");

        _teamMember._agentActions.AttackEnemy(Enemy);

        finished = true;
        return finished;
    }

    public override string ToString()
    {
        return "Fighting Enemy";
    }
}
