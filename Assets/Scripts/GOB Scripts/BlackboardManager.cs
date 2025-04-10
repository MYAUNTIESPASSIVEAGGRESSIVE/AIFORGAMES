using System;
using System.Collections.Generic;
using UnityEngine;

// Allows for multiple types of values to be included within the blackboard.
[Serializable]
public class BlackBoard
{
    // values
    Dictionary<string, object> GenericValue = new Dictionary<string, object>();
    Dictionary<string, GameObject> GOValue = new Dictionary<string, GameObject>();
    Dictionary<string, Vector3> V3Value = new Dictionary<string, Vector3>();
    Dictionary<string, int> IntValue = new Dictionary<string, int>();
    Dictionary<string, float> FloatValue = new Dictionary<string, float>();

    #region GenericValue
    public void SetGeneric(string ObjName, object Value)
    {
        GenericValue[ObjName] = Value;
    }

    public object GetGeneric(string ObjName)
    {
        return GenericValue[ObjName];
    }
    #endregion

    #region GameObjectValue
    public void SetGameObject(string ObjName, GameObject Value)
    {
        GOValue[ObjName] = Value;
    }

    public GameObject GetGameObject(string ObjName)
    {
        return GOValue[ObjName];
    }
    #endregion

    #region Vector3Value
    public void SetVector3(string ObjName, Vector3 Value)
    {
        V3Value[ObjName] = Value;
    }

    public Vector3 GetVector3(string ObjName)
    {
        return V3Value[ObjName];
    }
    #endregion
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
