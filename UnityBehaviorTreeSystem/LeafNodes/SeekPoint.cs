/*******************************************************************************
File: SeekPoint.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Leaf node, causes a given transform to pursue a referenced point. 
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeekPoint : Node
{
    private Vector2Reference TargetPoint;
    private Rigidbody2D RB;
    private FloatReference Speed;
    private Transform ObjectToMove;
    TextMeshPro DebugText;
    string DebugTitle;
    public SeekPoint(Vector2Reference targetPoint, Transform moveObject, 
            Rigidbody2D rb, FloatReference speed, TextMeshPro debugText, 
                                                        string debugTitle)
    {
        TargetPoint = targetPoint;
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
        if (TargetPoint == null)
            return returnState;
        DebugText.text = DebugTitle;
        returnState = ReturnStates.RUNNING;
        //Null guard
        if (RB == null)
            return returnState;
        if (Speed == null)
            return returnState;
        RB.Seek(ObjectToMove.position, TargetPoint.Value, Speed.Value);
        return ReturnState;
    }
}