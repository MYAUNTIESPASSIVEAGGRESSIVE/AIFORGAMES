using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAction : ActionBase
{
    public PowerUpAction(AI teamMember): base(teamMember)
    {
    }

    public override void Execute(float deltaTime)
    {
        // checks if the AI has the power up
        if (_teamMember._agentInventory.HasItem(Names.PowerUp))
        {
            UsePowerUp();
        }

        // if not it executes this
        CollectPowerUp();
    }

    // checks collectables in view to see which ones are power ups
    private bool CollectPowerUp()
    {
        List<GameObject> collectables = _teamMember._agentSenses.GetCollectablesInView();

        GameObject PowerUp;

        foreach (GameObject _collectable in collectables)
        {
            if (_collectable.GetComponent<PowerUp>())
            {
                // if it is a power up Go To function is activated + power up is passed through
                PowerUp = _collectable;
                GoToPowerUp(PowerUp);
                return finished;
            }
        }
        return false;
    }

    private void GoToPowerUp(GameObject _powerUp)
    {
        // moves to the power up and picks it up
        _teamMember._agentActions.MoveTo(_powerUp.transform.position);

        if (Vector3.Distance(_teamMember.transform.position, _powerUp.transform.position) <= 1)
        {
            _teamMember._agentActions.CollectItem(_powerUp);
        }
    }

    private bool UsePowerUp()
    {
        // uses power up
        _teamMember._agentActions.UseItem(_teamMember._agentInventory.GetItem(Names.PowerUp));

        finished = true;
        return finished;
    }

    public override string ToString()
    {
        return "PowerUp Actions";
    }
}
