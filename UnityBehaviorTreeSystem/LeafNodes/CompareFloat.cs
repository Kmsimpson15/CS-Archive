/*******************************************************************************
File: CompareFloat.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Leaf node, compares two float references, returns bool value of comparison.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used as means of passing comparitive signs as function parameters
public enum EqualSigns
{
    GreaterThan, LessThan, EqualTo,
    NotEqualTo, GreaterThanOrEqual, LessThenOrEqual
}
public class CompareFloat : Node
{
    private FloatReference Float1;
    private FloatReference Float2;
    private EqualSigns Sign;

    public CompareFloat(FloatReference float1, FloatReference float2, 
                                                    EqualSigns sign)
    {
        Float1 = float1;
        Float2 = float2;
        Sign = sign;
    }

    //Return first success for if all fail
    public override ReturnStates Evaluate()
    {
        returnState = ReturnStates.RUNNING;
        if (Float1 == null)
            return returnState;
        if (Float2 == null)
            return returnState;
        bool b = false;
        //Use enum to determine sign
        switch (Sign)
        {
            case EqualSigns.GreaterThan:
                b = Float1.Value > Float2.Value;
                break;
            case EqualSigns.GreaterThanOrEqual:
                b = Float1.Value >= Float2.Value;
                break;
            case EqualSigns.LessThan:
                b = Float1.Value < Float2.Value;
                break;
            case EqualSigns.LessThenOrEqual:
                b = Float1.Value <= Float2.Value;
                break;
            case EqualSigns.EqualTo:
                b = Mathf.Approximately(Float1.Value, Float2.Value);
                break;
            case EqualSigns.NotEqualTo:
                b = Float1.Value != Float2.Value;
                break;
            default:
                b = false;
                break;
        }
        if (b)
            returnState = ReturnStates.SUCCESS;
        else
            returnState = ReturnStates.FAILURE;

        return ReturnState;
    }
}
