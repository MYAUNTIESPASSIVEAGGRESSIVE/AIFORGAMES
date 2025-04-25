using System.Collections.Generic;
using UnityEngine;

public class MedKitAction : ActionBase
{
    public MedKitAction(AI teamMember) : base(teamMember)
    {
    }

    public override void Execute(float deltaTime)
    {
        if (_teamMember._agentInventory.HasItem("Health Kit"))
        {
            UseMedKit();
        }
        CollectMedKit();
    }

    private bool CollectMedKit()
    {
        List<GameObject> collectables = _teamMember._agentSenses.GetCollectablesInView();

        GameObject HealthKit;

        foreach (GameObject _collectable in collectables)
        {
            if (_collectable.GetComponent<HealthKit>())
            {
                HealthKit = _collectable;
                GoToHealthKit(HealthKit);
                return finished;
            }
        }
        return false;
    }

    private void GoToHealthKit(GameObject _healthKit)
    {
        _teamMember._agentActions.MoveTo(_healthKit.transform.position);

        if (Vector3.Distance(_teamMember.transform.position, _healthKit.transform.position) <= 1)
        {
            _teamMember._agentActions.CollectItem(_healthKit);
        }
    }

    private bool UseMedKit()
    {
        _teamMember._agentActions.UseItem(_teamMember._agentInventory.GetItem("Health Kit"));

        finished = true;
        return finished;
    }

    public override string ToString()
    {
        return "Med Kit Actions";
    }

}
