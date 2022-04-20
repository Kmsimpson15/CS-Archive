/*******************************************************************************
File: Sleep.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Leaf node, manages the sleeping behavior of an animal.  Animal sleeps for an
amount of time then has its seep counters reset.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sleep : Node
{
    private AnimalData AD;
    TextMeshPro DebugText;
    string DebugTitle;
    public Sleep(AnimalData ad, TextMeshPro debugText, string debugTitle)
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
        //Stop moving
        Rigidbody2D rb = AD.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;
        AD.SleepTimer.Value += Time.deltaTime;
        DebugText.text = DebugTitle;
        returnState = ReturnStates.RUNNING;
        //Restore sleep timers when sleep is over
        if (AD.SleepTimer.Value >= AD.SleepDuration.Value)
        {
            AD.CurrentSleep.Value = AD.MaxSleep.Value;
            AD.SleepTimer.Value = 0;
            returnState = ReturnStates.SUCCESS;
        }
        return ReturnState;
    }
}
