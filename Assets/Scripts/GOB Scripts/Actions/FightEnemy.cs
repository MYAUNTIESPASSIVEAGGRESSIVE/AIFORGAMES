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
        //_teamMember._agentActions.AttackEnemy(_teamMember._agentSenses.GetNearestEnemyInView());
    }

    public override string ToString()
    {
        return "Fighting Enemy";
    }
}
