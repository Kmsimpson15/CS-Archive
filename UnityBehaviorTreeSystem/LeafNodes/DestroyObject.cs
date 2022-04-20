/*******************************************************************************
File: DestroyObject.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Leaf node, destroy a given object, returns successful if object could be 
destroyed.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : Node
{
    private GameObject Target;
    public DestroyObject(GameObject target)
    {
        Target = target;
    }

    //Return first success for if all fail
    public override ReturnStates Evaluate()
    {
        returnState = ReturnStates.FAILURE;
        if (Target == null)
            return ReturnState;

        GameObject.Destroy(Target);
        returnState = ReturnStates.SUCCESS;
        return ReturnState;
    }
}
