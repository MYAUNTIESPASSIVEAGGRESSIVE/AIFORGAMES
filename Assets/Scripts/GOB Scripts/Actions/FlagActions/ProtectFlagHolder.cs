using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectFlagHolder : ActionBase
{
    public ProtectFlagHolder(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltatime)
    {
        if (TeamMateNear())
        {

        }
    }

    private bool TeamMateNear()
    {
        if (_teamMember._agentSenses.GetNearestFriendlyInView())
        {
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return "Protecting Flag Holder";
    }
}
