using UnityEngine;

public class ProtectFlagHolder : ActionBase
{
    public ProtectFlagHolder(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltatime)
    {
        // gets the nearest teammate
        //GameObject NearestTeamMate = _teamMember._agentSenses.GetNearestFriendlyInView();
        /*
        // checks if the teammate has a flag or not
        if (TeamMateNear(NearestTeamMate) != null)
        {
            // passes in the teammate GO
            ProtectTeamMate(NearestTeamMate);
        }
        */
    }

    private GameObject TeamMateNear(GameObject nearTeamMate)
    {
        // if the teammate has the enemy flag or friendly flag then returns the game object
        if (nearTeamMate.GetComponent<AgentData>().HasEnemyFlag || nearTeamMate.GetComponent<AgentData>().HasFriendlyFlag)
        {
            return nearTeamMate;
        }
        return null;
    }

    private bool ProtectTeamMate(GameObject flagBarer)
    {
        // checks the distance is shorter than 3 between AI and flag barer
        if (Vector3.Distance(flagBarer.transform.position, _teamMember.transform.position) <= 3)
        {
            // updates the goal with the distance value x10
            _teamMember.Gob_AI.UpdateGoals(2, 10 * Vector3.Distance(flagBarer.transform.position, _teamMember.transform.position));

            finished = true;
            return finished;
        }

        // if distance is long then move to flagbarer
        _teamMember._agentActions.MoveTo(flagBarer.transform.position);
        return finished;
    }

    public override string ToString()
    {
        return "Protecting Flag Holder";
    }
}
