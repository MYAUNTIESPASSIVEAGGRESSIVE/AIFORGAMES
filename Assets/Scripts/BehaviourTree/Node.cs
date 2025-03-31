using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


// Node states.
public enum NodeState
{
    Success,
    Running,
    Failed
}

public abstract class Node
{
    // current state of node.
    protected NodeState nodeState = NodeState.Failed;
    protected BlackBoard _blackBoard;

    protected Node(BlackBoard blackBoard)
    {
        _blackBoard = blackBoard;
    }

    public NodeState GetNodeState() { return nodeState; }

    // override by nodes to use!
    public abstract NodeState Evaluate();
}
