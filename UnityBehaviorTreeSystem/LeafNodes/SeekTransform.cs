/*******************************************************************************
File: SeekTransform.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Leaf node, causes a given transform to pursue another one. Pursuit is based on
the target transforms positon.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeekTransform : Node
{
    private TransformReference TargetTransform;
    private Rigidbody2D RB;
    private FloatReference Speed;
    private Transform ObjectToMove;
    TextMeshPro DebugText;
    string DebugTitle;

    public SeekTransform(TransformReference targetTransform, 
           Transform moveObject, Rigidbody2D rb, FloatReference speed, 
                            TextMeshPro debugText, string debugTitle)
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
        DebugText.text = DebugTitle;
        returnState = ReturnStates.RUNNING;
        //Null guards
        if (RB == null)
            return returnState;
        if (Speed == null)
            return returnState;
        Vector2 targetPos = TargetTransform.Value.position;
        RB.Seek(ObjectToMove.position, targetPos, Speed.Value);
        return ReturnState;
    }
}