using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : ActionBase
{
    public Flee(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltatime)
    {
        
    }

    public override string ToString()
    {
        return "Flee from the Danger";
    }
}
