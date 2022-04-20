/*******************************************************************************
File: SelectorNode.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Selector behavior tree node, runs each child node until one returns success,
if all childern fail then node returns failure.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : Node
{
    //Childern
    protected List<Node> Nodes = new List<Node>();


    public SelectorNode(List<Node> nodes)
    {
        Nodes = nodes;
    }

    //Return first success for if all fail
    public override ReturnStates Evaluate()
    {
        foreach (Node node in Nodes)
        {
            switch (node.Evaluate())
            {
                case ReturnStates.FAILURE:
                    continue;
                case ReturnStates.SUCCESS:
                    returnState = ReturnStates.SUCCESS;
                    return returnState;
                case ReturnStates.RUNNING:
                    returnState = ReturnStates.RUNNING;
                    return returnState;
                default:
                    continue;
            }
        }
        returnState = ReturnStates.FAILURE;
        return returnState;
    }
}
