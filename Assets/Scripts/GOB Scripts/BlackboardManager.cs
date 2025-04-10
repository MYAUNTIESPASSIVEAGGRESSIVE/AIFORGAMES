using System;
using System.Collections.Generic;
using UnityEngine;

// Allows for multiple types of values to be included within the blackboard.
[Serializable]
public class BlackBoard
{
    // values
    Dictionary<string, object> GenericValue = new Dictionary<string, object>();
}

// creates a singleton for the manager and holds a function to get the teams blackboard within the AI
public class BlackboardManager : MonoBehaviour
{
    public static BlackboardManager Instance { get; private set; }

    Dictionary<int, BlackBoard> SharedBlackBoard = new Dictionary<int, BlackBoard>();

    // ensures Blackboard Manager only has 1 instance.
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    public BlackBoard GetSharedBlackBoard(int ID)
    {
        if (!SharedBlackBoard.ContainsKey(ID))
        {
            return SharedBlackBoard[ID] = new BlackBoard();
        }

        return SharedBlackBoard[ID];
    }
}
