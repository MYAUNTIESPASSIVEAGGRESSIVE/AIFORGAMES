using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseMedKit : ActionBase
{
    public UseMedKit(AI teamMember) : base (teamMember)
    {

    }

    public override void Execute(float deltaTime)
    {
        if(HasMedKit())
        {
            //TeamMember._agentActions.UseItem(HealthKit);
        }
        else
        {
            _teamMember.Gob_AI.UpdateGoals(1, _teamMember._agentData.CurrentHitPoints);
        }
    }

    private bool HasMedKit()
    {
        if(_teamMember._agentInventory.HasItem("HealthKit")) return true;

        return false;
    }

    public override string ToString()
    {
        return "Use HealthKit";
    }
}
