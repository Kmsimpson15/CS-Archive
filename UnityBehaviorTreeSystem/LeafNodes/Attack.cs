/*******************************************************************************
File: Attack.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Manages attacks between an AI and its current attacker or prey.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Attack : Node
{
    private AnimalData AD;
    TextMeshPro DebugText;
    string DebugTitle;
    bool AttackPrey = true;

    public Attack(AnimalData ad, TextMeshPro debugText, string debugTitle, 
                                                            bool attackPrey)
    {
        AD = ad;
        DebugText = debugText;
        DebugTitle = debugTitle;
        AttackPrey = attackPrey;
    }

    //Return first success for if all fail
    public override ReturnStates Evaluate()
    {
        returnState = ReturnStates.FAILURE;
        if (AD == null)
            return ReturnState;
        //Null checks based on if attack is offensive or defensive
        if (AttackPrey)
        {
            if (AD.NearestPrey.Value == null)
                return ReturnState;
        }
        else
        {
            if (AD.CurrentAttacker.Value == null)
                return ReturnState;
        }
        AnimalData data;
        if (AttackPrey)
            data = AD.NearestPrey.Value.GetComponent<AnimalData>();
        else
            data = AD.CurrentAttacker.Value.GetComponent<AnimalData>();
        if (data == null)
            return ReturnState;
        Vector2 pos = AD.transform.position;
        float d = Vector2.Distance(pos, data.transform.position);
        if (d > AD.AttackRange.Value)
            return ReturnState;
        else
        {
            AD.AttackTimer.Value += Time.deltaTime;
            returnState = ReturnStates.RUNNING;
            DebugText.text = DebugTitle;
            //Attack after each cooldown period
            if (AD.AttackTimer.Value >= AD.AttackCoolDown.Value)
            {
                data.CurrentHealth.Value -= AD.AttackDamage.Value;
                data.CurrentAttacker.Value = AD.transform;
                AD.AttackTimer.Value = 0;
                returnState = ReturnStates.SUCCESS;
            }
        }
        return ReturnState;
    }
}
