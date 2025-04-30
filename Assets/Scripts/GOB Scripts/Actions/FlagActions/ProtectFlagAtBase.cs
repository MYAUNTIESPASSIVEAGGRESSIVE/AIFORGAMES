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
        // move to base
        _teamMember._agentActions.MoveTo(_teamMember._agentData.FriendlyBase.transform.position);

        // return true
        finished = true;
        return finished;
    }

    public override string ToString()
    {
        return "Protecting Base";
    }
}
