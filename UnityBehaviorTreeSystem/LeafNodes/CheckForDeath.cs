/*******************************************************************************
File: CheckForDeath.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Leaf node, checks health and hunger of an animal Ai to determine if it is
alive or not.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckForDeath : Node
{
    private AnimalData AD;
    TextMeshPro DebugText;
    string DebugTitle;
    public CheckForDeath(AnimalData ad, TextMeshPro debugText, 
                                                        string debugTitle)
    {
        AD = ad;
        DebugText = debugText;
        DebugTitle = debugTitle;
    }

    //Return first success for if all fail
    public override ReturnStates Evaluate()
    {
        returnState = ReturnStates.FAILURE;
        if (AD == null)
            return ReturnState;
        //If hunger dips 2X max below zero then animal dies
        bool b = AD.CurrentHunger.Value <= -AD.MaxHunger.Value * 2;
        if (AD.CurrentHealth.Value <= 0 || b)
        {
            AD.Calories.Value -= Time.deltaTime * AD.DecayRate;
            DebugText.text = DebugTitle;
            AD.CurrentHealth.Value = 0;
            Rigidbody2D rb = AD.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = Vector2.zero;
            returnState = ReturnStates.SUCCESS;
        }
        return ReturnState;
    }
}
