using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEnemyFlag : ActionBase
{
    public GetEnemyFlag(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltaTime)
    {

    }

    public override string ToString()
    {
        return "Get Enemy Flag";
    }
}
