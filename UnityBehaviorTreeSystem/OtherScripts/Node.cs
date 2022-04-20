/*******************************************************************************
File:      Node.cs
Author:    Kaelan Simpson
DP Email:  kaelan.simpson@digipen.edu
Date:      4/10/2021
Course:    CS199
Section:   A

Description:
    Base Node class conains a return state varible and evaluate funciton.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ReturnStates {RUNNING, SUCCESS, FAILURE}
[System.Serializable]
public abstract class Node
{

    //Returns node state
    public delegate ReturnStates NodeReturn();

    //Current node state
    protected ReturnStates returnState;

    public ReturnStates ReturnState
    {
        get { return returnState; }
    }

    //Constructor
    public Node() 
    { 

    }

   //Evalute set of conditions
    public abstract ReturnStates Evaluate();

}
