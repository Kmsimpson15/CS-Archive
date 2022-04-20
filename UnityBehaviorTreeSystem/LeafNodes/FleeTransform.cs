/*******************************************************************************
File:FleeTransform.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Leaf node, causes a given transform to flee another one. Flight is based on
the target transforms positon.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FleeTransform : Node
{
    private TransformReference TargetTransform;
    private Rigidbody2D RB;
    private FloatReference Speed;
    private Transform ObjectToMove;
    TextMeshPro DebugText;
    string DebugTitle;

    public FleeTransform(TransformReference targetTransform, 
                                Transform moveObject, Rigidbody2D rb, 
                            FloatReference speed, TextMeshPro debugText, 
                                                        string debugTitle)
    {
        TargetTransform = targetTransform;
        RB = rb;
        Speed = speed;
        ObjectToMove = moveObject;
        DebugText = debugText;
        DebugTitle = debugTitle;
    }

    //Return first success for if all fail
    public override ReturnStates Evaluate()
    {
        returnState = ReturnStates.FAILURE;
        if (TargetTransform == null)
            return returnState;
        if (TargetTransform.Value == null)
            return returnState;
        returnState = ReturnStates.RUNNING;
        DebugText.text = DebugTitle;
        if (RB == null)
            return returnState;
        if(Speed == null)
            return returnState;
        Vector2 pos = TargetTransform.Value.position;
        RB.Flee(ObjectToMove.position, pos, Speed.Value);
        return ReturnState;
    }
}
