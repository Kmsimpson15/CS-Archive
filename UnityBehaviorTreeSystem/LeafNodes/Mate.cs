/*******************************************************************************
File: Mate.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Leaf node, manages reproduction for animal AI, requires parter or correct
species and sex to complete reproducetion timer.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mate : Node
{
    AnimalData AD;
    TextMeshPro DebugText;
    string DebugTitle;

    public Mate(AnimalData ad, TextMeshPro debugText, string debugTitle)
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
        AD.isMating = false;
        if (AD.NearestMate.Value == null)
        {
            AD.MateTimer.Value = 0;
            return ReturnState;
        }
        AnimalData data = AD.NearestMate.Value.GetComponent<AnimalData>();
        if (data == null)
        {
            AD.MateTimer.Value = 0;
            return ReturnState;
        }
        Vector2 matePos = AD.NearestMate.Value.position;
        float d = Vector2.Distance(AD.transform.position, matePos);
        if (d > AD.MateRange.Value)
        {
            AD.MateTimer.Value = 0;
            return ReturnState;
        }
        else
        {
            //Stop movement
            Rigidbody2D rb = AD.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = Vector2.zero;
            AD.MateTimer.Value += Time.deltaTime;
            AD.isMating = true;
            DebugText.text = DebugTitle;
            returnState = ReturnStates.RUNNING;
            float md = data.MateDuration.Value;
            //Get the longer mate time and use that for both partners
            float dur = AD.MateDuration.Value.ReturnGreater(md);
            if (AD.MateTimer.Value >= dur)
            {
                bool cr = AD.CanReproduce;
                //make sure animal has enough food to produce offspring
               if (cr && AD.CurrentHunger.Value > -AD.MaxHunger.Value)
                {
                    Quaternion rot = AD.transform.rotation;
                    Vector2 pos = AD.transform.position;
                    GameObject a;
                    a = GameObject.Instantiate(AD.OffSpringPrefab, pos, rot);
                    AnimalData ad = a.GetComponent<AnimalData>();
                    if (ad != null)
                        ad.GD_Inherited = AD.GD_Current;
                }
                AD.MateTimer.Value = 0;
                AD.CurrentMate.Value = AD.MaxMate.Value;
                returnState = ReturnStates.SUCCESS;
                AD.isMating = false;
            }
        }
        return ReturnState;
    }
}

