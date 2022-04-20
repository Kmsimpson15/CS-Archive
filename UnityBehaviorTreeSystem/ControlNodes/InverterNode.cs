/*******************************************************************************
File: InverterNode.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Inverter behavior tree node, returns the opposite of its child's return value.
*******************************************************************************/
using UnityEngine;
using System.Collections;

public class InverterNode : Node
{
    //Child
    private Node childNode;

    public Node ChildNode
    {
        get { return childNode; }
    }

    public InverterNode(Node node)
    {
        childNode = node;
    }

    //Success = fail, fail = success
    public override ReturnStates Evaluate()
    {
        switch (childNode.Evaluate())
        {
            case ReturnStates.FAILURE:
                returnState = ReturnStates.SUCCESS;
                return returnState;
            case ReturnStates.SUCCESS:
                returnState = ReturnStates.FAILURE;
                return returnState;
            case ReturnStates.RUNNING:
                returnState = ReturnStates.RUNNING;
                return returnState;
        }
        returnState = ReturnStates.SUCCESS;
        return returnState;
    }
}
