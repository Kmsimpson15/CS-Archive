/*******************************************************************************
File: AlwaysFailureNode.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Consistance failure behavior tree node, always returns failure.
*******************************************************************************/
using UnityEngine;
using System.Collections;

public class AlwaysFailureNode : Node
{
    //Child
    private Node childNode;

    public Node ChildNode
    {
        get { return childNode; }
    }

    public AlwaysFailureNode(Node node)
    {
        childNode = node;
    }

    //Success = fail, fail = success
    public override ReturnStates Evaluate()
    {
        returnState = ReturnStates.FAILURE;
        childNode.Evaluate();
        return returnState;
    }
}
