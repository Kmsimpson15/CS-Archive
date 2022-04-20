/*******************************************************************************
File: SequenceNode.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Sequence behavior tree node, runs each child node until all are successfull.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : Node
{
    //Childern
    private List<Node> Nodes = new List<Node>();

    public SequenceNode(List<Node> nodes)
    {
        Nodes = nodes;
    }

    //Returns fail one first fail and success if all are successful
    public override ReturnStates Evaluate()
    {
        bool childRunning = false;
        foreach (Node node in Nodes)
        {
            switch (node.Evaluate())
            {
                case ReturnStates.FAILURE:
                    returnState = ReturnStates.FAILURE;
                    return returnState;
                case ReturnStates.SUCCESS:
                    continue;
                case ReturnStates.RUNNING:
                    childRunning = true;
                    return ReturnStates.RUNNING;
                default:
                    returnState = ReturnStates.SUCCESS;
                    return returnState;
            }
        }
        returnState = childRunning ? ReturnStates.RUNNING : ReturnStates.SUCCESS;
        return returnState;
    }
}