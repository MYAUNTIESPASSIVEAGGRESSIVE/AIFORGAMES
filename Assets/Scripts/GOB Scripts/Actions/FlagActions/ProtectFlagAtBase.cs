using UnityEngine;

public class ProtectFlagAtBase : ActionBase
{
    public ProtectFlagAtBase(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltatime)
    {
        StayAtBase();
    }

    // if the flags are both inside the base then move to the base.
    public bool StayAtBase()
    {
        // local GO variables for ease
        GameObject FriendlyFlag = _teamMember._agentData.FriendlyFlag;
        GameObject EnemyFlag = _teamMember._agentData.EnemyFlag;
        GameObject FriendlyBase = _teamMember._agentData.FriendlyBase;

        // gets if both flags are within the base
        if (Vector3.Distance(FriendlyFlag.transform.position, EnemyFlag.transform.position) <= 2 &&
            Vector3.Distance(FriendlyFlag.transform.position, FriendlyFlag.transform.position) <= 2)
        {
            // move to base
            _teamMember._agentActions.MoveTo(FriendlyBase.transform.position);

            // return true
            finished = true;
            return finished;
        }

        // return false and continue moving
        return finished;
    }

    public override string ToString()
    {
        return "Protecting Base";
    }
}
