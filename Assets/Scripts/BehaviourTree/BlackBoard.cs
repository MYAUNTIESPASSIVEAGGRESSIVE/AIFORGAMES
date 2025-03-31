using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using UnityEditor.EditorTools;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
/*
 * The blackboard within this AI uses a hash map to signify
 * locations of AI or entities.
 * 
 * Also holds the state of AI (dead or alive) + the game (winning or losing)
 */

public enum BlackBoardKey
{

}

// Allows for multiple types of values to be included within the blackboard.
[Serializable]
public class BlackBoard
{
    // values
    Dictionary<BlackBoardKey, string> StringValue = new Dictionary<BlackBoardKey, string>();
    Dictionary<BlackBoardKey, int> IntValue = new Dictionary<BlackBoardKey, int>();
    Dictionary<BlackBoardKey, float> FloatValue = new Dictionary<BlackBoardKey, float>();
    Dictionary<BlackBoardKey, bool> BoolValue = new Dictionary<BlackBoardKey, bool>();
    Dictionary<BlackBoardKey, Vector3> V3Value = new Dictionary<BlackBoardKey, Vector3>();
    Dictionary<BlackBoardKey, GameObject> GOValue = new Dictionary<BlackBoardKey, GameObject>();
    Dictionary<BlackBoardKey, object> GenericValue = new Dictionary<BlackBoardKey, object>();

    // set functions for each value.

    private bool TryGet<T>(Dictionary<BlackBoardKey, T> blackboard, BlackBoardKey key, out T value, T defaultValue = default)
    {
        if (blackboard.ContainsKey(key))
        {
            value = blackboard[key];
            return true;
        }

        value = defaultValue;
        return false;
    }

    // generic
    public void SetGeneric<T>(BlackBoardKey key, T value)
    {
        GenericValue[key] = value;
    }

    public T GetGeneric<T>(BlackBoardKey key) where T : class
    {
        return GenericValue[key] as T;
    }
    // string
    public void Set(BlackBoardKey key, string value)
    {
        StringValue[key] = value;
    }

    public bool TryGet(BlackBoardKey key, out string value, string defaultValue = null)
    {
        return TryGet<string>(StringValue, key, out value, defaultValue);
    }

    public string GetString(BlackBoardKey key)
    {
        return StringValue[key];
    }

    // int
    public void Set(BlackBoardKey key, int value)
    {
        IntValue[key] = value;
    }

    public bool TryGet(BlackBoardKey key, out int value, int defaultValue = default)
    {
        return TryGet<int>(IntValue, key, out value, defaultValue);
    }

    public int GetInt(BlackBoardKey key)
    {
        return IntValue[key];
    }

    // float
    public void Set(BlackBoardKey key, float value)
    {
        FloatValue[key] = value;
    }

    public bool TryGet(BlackBoardKey key, out float value, float defaultValue = default)
    {
        return TryGet<float>(FloatValue, key, out value, defaultValue);
    }

    public float GetFloat(BlackBoardKey key)
    {
        return FloatValue[key];
    }

    // bool
    public void Set(BlackBoardKey key, bool value)
    {
        BoolValue[key] = value;
    }

    public bool TryGet(BlackBoardKey key, out bool value, bool defaultValue = default)
    {
        return TryGet<bool>(BoolValue, key, out value, defaultValue);
    }

    public bool GetBool(BlackBoardKey key)
    {
        return BoolValue[key];
    }

    // Vector3
    public void Set(BlackBoardKey key, Vector3 value)
    {
        V3Value[key] = value;
    }

    public bool TryGet(BlackBoardKey key, out Vector3 value, Vector3 defaultValue = default)
    {
        return TryGet<Vector3>(V3Value, key, out value, defaultValue);
    }

    public Vector3 GetVector3(BlackBoardKey key)
    {
        return V3Value[key];
    }

    // GameObject
    public void Set(BlackBoardKey key, GameObject value)
    {
        GOValue[key] = value;
    }
    public bool TryGet(BlackBoardKey key, out GameObject value, GameObject defaultValue = default)
    {
        return TryGet<GameObject>(GOValue, key, out value, defaultValue);
    }

    public GameObject GetGameObject(BlackBoardKey key)
    {
        return GOValue[key];
    }
}

public class BlackboardManager : MonoBehaviour
{
    public static BlackboardManager Instance { get; private set; } = null;

    // dictionary which holds information for blackboards for individual AI and the entire Team
    Dictionary<GameObject, BlackBoard> IndividBlackBoard = new Dictionary<GameObject, BlackBoard>();
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


    public BlackBoard GetIndividualBlackBoard(GameObject Entity)
    {
        if (!IndividBlackBoard.ContainsKey(Entity))
        {
            return IndividBlackBoard[Entity] = new BlackBoard();
        }

        return IndividBlackBoard[Entity];
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
