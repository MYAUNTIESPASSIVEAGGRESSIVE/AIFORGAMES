using System.Collections.Generic;
using UnityEngine;

public class MedKitAction : ActionBase
{
    public MedKitAction(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltaTime)
    {
        // checks if the AI has the medpack
        if (_teamMember._agentInventory.HasItem(Names.HealthKit))
        {
            UseMedKit();
        }

        // if not it executes this
        CollectMedKit();
    }

    // checks collectables in view to see which ones are health kits
    private bool CollectMedKit()
    {
        List<GameObject> collectables = _teamMember._agentSenses.GetCollectablesInView();

        GameObject HealthKit;

        foreach (GameObject _collectable in collectables)
        {
            if (_collectable.GetComponent<HealthKit>())
            {
                // if it is a health kit Go To function is activated + healthkit is passed through
                HealthKit = _collectable;
                GoToHealthKit(HealthKit);
                return finished;
            }
        }
        return false;
    }

    private void GoToHealthKit(GameObject _healthKit)
    {
        // moves to the health kit and picks it up
        _teamMember._agentActions.MoveTo(_healthKit.transform.position);

        if (Vector3.Distance(_teamMember.transform.position, _healthKit.transform.position) <= 1)
        {
            _teamMember._agentActions.CollectItem(_healthKit);
        }
    }

    private bool UseMedKit()
    {
        // uses the health kit
        _teamMember._agentActions.UseItem(_teamMember._agentInventory.GetItem(Names.HealthKit));

        finished = true;
        return finished;
    }

    public override string ToString()
    {
        return "Med Kit Actions";
    }

}
