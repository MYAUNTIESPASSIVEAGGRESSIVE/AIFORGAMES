using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

/*****************************************************************************************************************************
 * Write your core AI code in this file here. The private variable 'agentScript' contains all the agents actions which are listed
 * below. Ensure your code it clear and organised and commented.
 *
 * Unity Tags
 * public static class Tags
 * public const string BlueTeam = "Blue Team";	The tag assigned to blue team members.
 * public const string RedTeam = "Red Team";	The tag assigned to red team members.
 * public const string Collectable = "Collectable";	The tag assigned to collectable items (health kit and power up).
 * public const string Flag = "Flag";	The tag assigned to the flags, blue or red.
 * 
 * Unity GameObject names
 * public static class Names
 * public const string PowerUp = "Power Up";	Power up name
 * public const string HealthKit = "Health Kit";	Health kit name.
 * public const string BlueFlag = "Blue Flag";	The blue teams flag name.
 * public const string RedFlag = "Red Flag";	The red teams flag name.
 * public const string RedBase = "Red Base";    The red teams base name.
 * public const string BlueBase = "Blue Base";  The blue teams base name.
 * public const string BlueTeamMember1 = "Blue Team Member 1";	Blue team member 1 name.
 * public const string BlueTeamMember2 = "Blue Team Member 2";	Blue team member 2 name.
 * public const string BlueTeamMember3 = "Blue Team Member 3";	Blue team member 3 name.
 * public const string RedTeamMember1 = "Red Team Member 1";	Red team member 1 name.
 * public const string RedTeamMember2 = "Red Team Member 2";	Red team member 2 name.
 * public const string RedTeamMember3 = "Red Team Member 3";	Red team member 3 name.
 * 
 * _agentData properties and public variables
 * public bool IsPoweredUp;	Have we powered up, true if we’re powered up, false otherwise.
 * public int CurrentHitPoints;	Our current hit points as an integer
 * public bool HasFriendlyFlag;	True if we have collected our own flag
 * public bool HasEnemyFlag;	True if we have collected the enemy flag
 * public GameObject FriendlyBase; The friendly base GameObject
 * public GameObject EnemyBase;    The enemy base GameObject
 * public int FriendlyScore; The friendly teams score
 * public int EnemyScore;       The enemy teams score
 * 
 * _agentActions methods
 * public bool MoveTo(GameObject target)	Move towards a target object. Takes a GameObject representing the target object as a parameter. Returns true if the location is on the navmesh, false otherwise.
 * public bool MoveTo(Vector3 target)	Move towards a target location. Takes a Vector3 representing the destination as a parameter. Returns true if the location is on the navmesh, false otherwise.
 * public bool MoveToRandomLocation()	Move to a random location on the level, returns true if the location is on the navmesh, false otherwise.
 * public void CollectItem(GameObject item)	Pick up an item from the level which is within reach of the agent and put it in the inventory. Takes a GameObject representing the item as a parameter.
 * public void DropItem(GameObject item)	Drop an inventory item or the flag at the agents’ location. Takes a GameObject representing the item as a parameter.
 * public void UseItem(GameObject item)	Use an item in the inventory (currently only health kit or power up). Takes a GameObject representing the item to use as a parameter.
 * public void AttackEnemy(GameObject enemy)	Attack the enemy if they are close enough. ). Takes a GameObject representing the enemy as a parameter.
 * public void Flee(GameObject enemy)	Move in the opposite direction to the enemy. Takes a GameObject representing the enemy as a parameter.
 * 
 * _agentSenses properties and methods
 * public List<GameObject> GetObjectsInViewByTag(string tag)	Return a list of objects with the same tag. Takes a string representing the Unity tag. Returns null if no objects with the specified tag are in view.
 * public GameObject GetObjectInViewByName(string name)	Returns a specific GameObject by name in view range. Takes a string representing the objects Unity name as a parameter. Returns null if named object is not in view.
 * public List<GameObject> GetObjectsInView()	Returns a list of objects within view range. Returns null if no objects are in view.
 * public List<GameObject> GetCollectablesInView()	Returns a list of objects with the tag Collectable within view range. Returns null if no collectable objects are in view.
 * public List<GameObject> GetFriendliesInView()	Returns a list of friendly team AI agents within view range. Returns null if no friendlies are in view.
 * public List<GameObject> GetEnemiesInView()	Returns a list of enemy team AI agents within view range. Returns null if no enemies are in view.
 * public GameObject GetNearestEnemyInView()   Returns the nearest enemy AI in view to the agent. Returns null if no enemies are in view.
 * public bool IsItemInReach(GameObject item)	Checks if we are close enough to a specific collectible item to pick it up), returns true if the object is close enough, false otherwise.
 * public bool IsInAttackRange(GameObject target)	Check if we're with attacking range of the target), returns true if the target is in range, false otherwise.
 * public Vector3 GetVectorToTarget(GameObject target)  Return a normalised vector pointing to the target GameObject
 * public Vector3 GetVectorToTarget(Vector3 target)     Return a normalised vector pointing to the target vector
 * public Vector3 GetFleeVectorFromTarget(GameObject target)    Return a normalised vector pointing away from the target GameObject
 * public Vector3 GetFleeVectorFromTarget(Vector3 target)   Return a normalised vector pointing away from the target vector
 * 
 * _agentInventory properties and methods
 * public bool AddItem(GameObject item)	Adds an item to the inventory if there's enough room (max capacity is 'Constants.InventorySize'), returns true if the item has been successfully added to the inventory, false otherwise.
 * public GameObject GetItem(string itemName)	Retrieves an item from the inventory as a GameObject, returns null if the item is not in the inventory.
 * public bool HasItem(string itemName)	Checks if an item is stored in the inventory, returns true if the item is in the inventory, false otherwise.
 * 
 * You can use the game objects name to access a GameObject from the sensing system. Thereafter all methods require the GameObject as a parameter.
 * 
*****************************************************************************************************************************/

/// <summary>
/// Implement your AI script here, you can put everything in this file, or better still, break your code up into multiple files
/// and call your script here in the Update method. This class includes all the data members you need to control your AI agent
/// get information about the world, manage the AI inventory and access essential information about your AI.
///
/// You may use any AI algorithm you like, but please try to write your code properly and professionaly and don't use code obtained from
/// other sources, including the labs.
///
/// See the assessment brief for more details
/// </summary>
public class AI : MonoBehaviour
{
    #region Other Agent Scripts
    [HideInInspector]
    // Gives access to important data about the AI agent (see above)
    public AgentData _agentData;

    [HideInInspector]
    // Gives access to the agent senses
    public Sensing _agentSenses;

    [HideInInspector]
    // gives access to the agents inventory
    public InventoryController _agentInventory;

    [HideInInspector]
    // This is the script containing the AI agents actions
    // e.g. agentScript.MoveTo(enemy);
    public AgentActions _agentActions;
    #endregion

    // this is a list of Scriptable Objects which the goals use for data.
    [Header("Goals")]
    public List<SO_Goals> Goals = new List<SO_Goals>();

    // Goal oriented AI script intialisation
    private GOB_AI _AI = new GOB_AI();

    // Goal oriented AI script getting
    public GOB_AI Gob_AI
    {
        get { return _AI; }
    }

    // keeps a hold of the target enemy for actions to access
    [HideInInspector]
    public GameObject TargetEnemy;

    // Use this for initialization and setting up goals and actions
    // they are then added to the main GOB_AI script's lists
    void Awake()
    {
        #region AgentInitialization
        // Initialise the accessable script components
        _agentData = GetComponent<AgentData>();
        _agentActions = GetComponent<AgentActions>();
        _agentSenses = GetComponentInChildren<Sensing>();
        _agentInventory = GetComponentInChildren<InventoryController>();
        #endregion

        #region GoalAdding
        ///<summary>
        /// creating a new goal using the Scriptable Object and AgentData. (noted with the letter G)
        /// </summary>

        // Get Enemy Flag
        GoalBase GEnemyFlag = new(GotFlag(), Goals[0], CurveFunctions.ReverseLinear);
        _AI.AddGoal(GEnemyFlag);

        // Protect Flag Holder
        GoalBase GProtectFlagHolder = new(TeamMateHasFlag(), Goals[1], CurveFunctions.StepAtUpper);
        _AI.AddGoal(GProtectFlagHolder);

        // Attack Nearby Enemy
        GoalBase GAttackEnemy = new(DistanceBetweenEnemy(), Goals[2], CurveFunctions.Exponential);
        _AI.AddGoal(GAttackEnemy);

        // Keep Health high
        GoalBase GKeepHealth = new(_agentData.CurrentHitPoints, Goals[3], CurveFunctions.ReverseLinear);
        _AI.AddGoal(GKeepHealth);

        // Keep the friendly flag at base
        GoalBase GKeepFriendlyFlag = new(GotFlag() + FlagDistance(), Goals[4], CurveFunctions.ReverseLinear);
        _AI.AddGoal(GKeepFriendlyFlag);

        // Keep both flags at base
        GoalBase GKeepFlagsAtBase = new(FlagDistance(), Goals[5], CurveFunctions.ReverseLinear);
        _AI.AddGoal(GKeepFlagsAtBase);

        // Get a powerup
        GoalBase GGeetPowerUp = new(_agentData.NormalAttackDamage, Goals[6], CurveFunctions.ReverseLinear);
        _AI.AddGoal(GGeetPowerUp);

        #endregion

        #region ActionAdding
        ///<summary>
        /// creating a new action used to satisfy goals
        ///</summary>

        // Get Enemy Flag + Return it
        GetEnemyFlag getEFlag = new(this);
        getEFlag.SetGoalSatifiaction(1, 100);
        _AI.AddAction(getEFlag);

        // Protect Flag Holder
        ProtectFlagHolder protectFlagHolder = new(this);
        protectFlagHolder.SetGoalSatifiaction(2, 250);
        _AI.AddAction(protectFlagHolder);

        // Attack Enemy
        FightEnemy fightEnemy = new(this);
        fightEnemy.SetGoalSatifiaction(3, 50);
        _AI.AddAction(fightEnemy);

        // Medkit find and use
        MedKitAction medkitActions = new(this);
        medkitActions.SetGoalSatifiaction(4, 500);
        _AI.AddAction(medkitActions);

        // Protect flags at base.
        ProtectFlagAtBase protectFlagAtBase = new(this);
        protectFlagAtBase.SetGoalSatifiaction(5, 500);
        protectFlagAtBase.SetGoalSatifiaction(6, 500);
        _AI.AddAction(protectFlagAtBase);

        // Find and return friendly flag
        FindTeamFlag findTeamFlag = new(this);
        findTeamFlag.SetGoalSatifiaction(5, 500);
        findTeamFlag.SetGoalSatifiaction(6, 500);
        _AI.AddAction(findTeamFlag);

        // Powerup find and use
        PowerUpAction powerUpAction = new(this);
        powerUpAction.SetGoalSatifiaction(7, 500);
        _AI.AddAction(powerUpAction);
        #endregion
    }

    // Update is called once per frame
    public void Update()
    {
        Debug.Log(this.name);

        #region Goal Updating
        _AI.UpdateGoals(1, GotFlag());

        _AI.UpdateGoals(4, DistanceBetweenEnemy());

        _AI.UpdateGoals(5, 10 * _agentData.CurrentHitPoints);

        _AI.UpdateGoals(6, FlagDistance());

        _AI.UpdateGoals(7, FlagDistance());
        #endregion

        // Run your AI code in here
        ActionBase currentAction = _AI.ChooseAction(this);
        //Debug.Log("Update: currentAction = " + currentAction.ToString());
        currentAction.Execute(Time.deltaTime);
    }

    #region Goal Value Functions
    // returns a float when the AI has the flag
    public float GotFlag()
    {
        if (_agentData.HasEnemyFlag || _agentData.HasFriendlyFlag)
        {
            return 200;
        }
        return 0;
    }

    // returns a float when an AI's teammate has the flag
    public float TeamMateHasFlag()
    {
        List<GameObject> TeamMates = _agentSenses.GetFriendliesInView();

        foreach (GameObject TeamMember in TeamMates)
        {
            if (TeamMember.GetComponentInChildren<InventoryController>().HasItem(_agentData.EnemyFlagName) ||
                TeamMember.GetComponentInChildren<InventoryController>().HasItem(_agentData.FriendlyFlagName))
            {
                return 100;
            }

            return 0;
        }

        return 0;
    }

    // returns a float when either friendly flag is outside the base
    // or both flags are inside the base
    public float FlagDistance()
    {
        if (Vector3.Distance(_agentData.FriendlyFlag.transform.position, _agentData.FriendlyBase.transform.position) > 2)
        {
            return 200;
        }

        if(Vector3.Distance(_agentData.EnemyFlag.transform.position, _agentData.FriendlyBase.transform.position) <= 2)
        {
            return 200;
        }

        if (Vector3.Distance(_agentData.FriendlyFlag.transform.position, _agentData.FriendlyBase.transform.position) <= 2 &&
            Vector3.Distance(_agentData.EnemyFlag.transform.position, _agentData.FriendlyBase.transform.position) <= 2)
        {
            return 800;
        }

        return 0;
    }

    // returns the distance between the AI and the nearest Enemy as a float
    public float DistanceBetweenEnemy()
    {
        GameObject nearestEnemy = _agentSenses.GetNearestEnemyInView();

        if (nearestEnemy != null)
        {
            TargetEnemy = nearestEnemy;

            return 800 * (1 / Vector3.Distance(transform.position, nearestEnemy.transform.position));
        }

        return 0;
    }

    #endregion
}