/*******************************************************************************
File: ConsumeCalories.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Leaf node, manages an animal AI's consumption of another AI suing their 
AnimalStats.  If successful the cosumer has its hunger restored and the 
consumed has its calories decreased.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsumeCalories : Node
{
    private AnimalData AD;
    TextMeshPro DebugText;
    string DebugTitle;

    public ConsumeCalories(AnimalData ad, TextMeshPro debugText, 
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
        if (AD.NearestFood.Value == null)
            return ReturnState;
        AnimalData data = AD.NearestFood.Value.GetComponent<AnimalData>();
        if (data == null)
            return ReturnState;
        Vector2 nPos = AD.NearestFood.Value.position;
        float d = Vector2.Distance(AD.transform.position, nPos);
        if (d > AD.EatRange.Value)
            return ReturnState;
        else
        {
            Rigidbody2D rb = AD.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = Vector2.zero;
            AD.EatTimer.Value += Time.deltaTime;
            returnState = ReturnStates.RUNNING;
            DebugText.text = DebugTitle;
            //Only take the calories when eating timer is up
            if (AD.EatTimer.Value >= AD.EatDuration.Value)
            {
                AD.CurrentHunger.Value = 0;
                //Absorb the calories from the consumed so other AI can't..
                //..All eat the saem thing
                AD.CurrentHunger.Absorb(data.Calories, AD.MaxHunger.Value);
                AD.EatTimer.Value = 0;
                returnState = ReturnStates.SUCCESS;
            }
        }
        return ReturnState;
    }
}
