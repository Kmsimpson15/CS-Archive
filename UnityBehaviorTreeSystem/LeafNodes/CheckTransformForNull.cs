/*******************************************************************************
File: CheckTransformForNull.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Leaf node, checks a transform reference to see if its null.  Returns success
if transform reference is null.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTransformForNull : Node
{
    private TransformReference Target;

    public CheckTransformForNull(TransformReference target)
    {
        Target = target;
    }

    //Return first success for if all fail
    public override ReturnStates Evaluate()
    {
        returnState = ReturnStates.SUCCESS;
        if (Target == null)
            return ReturnState;
        if (Target.Value == null)
            return ReturnState;

        returnState = ReturnStates.FAILURE;
        return ReturnState;
    }
}
