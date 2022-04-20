/*******************************************************************************
File: PlantMate.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Leaf node, manages reproduction for plant based AI, skips steps requiring 
a partner and spawns the offspring nearby instead of at parent location.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantMate : Node
{
    AnimalData AD;
    TextMeshPro DebugText;
    Vector2 MaxBound;
    Vector2 MinBound;
    SunData SD;
    DisplayHUD DH;

    public PlantMate(AnimalData ad, TextMeshPro debugText, Vector2 maxBound, 
                                                          Vector2 minBound)
    {
        AD = ad;
        DebugText = debugText;
        MaxBound = maxBound;
        MinBound = minBound;
        SD = GameObject.FindObjectOfType<SunData>();
        DH = GameObject.FindObjectOfType<DisplayHUD>();
    }

    //Return first success for if all fail
    public override ReturnStates Evaluate()
    {
        returnState = ReturnStates.FAILURE;
        if (AD == null)
            return ReturnState;
        //Start mating timer right away
        AD.MateTimer.Value += Time.deltaTime;
        AD.isMating = true;
        DebugText.text = "Reproduce";
        returnState = ReturnStates.RUNNING;
        if (AD.MateTimer.Value >= AD.MateDuration.Value)
        {
            Vector2 pos = AD.transform.position;
            float r = AD.SenseRange.Value;
            Vector2 spawnPos = pos + (Vector2)Random.insideUnitSphere * r;
            bool xCheck;
            xCheck = spawnPos.x >= MinBound.x && spawnPos.x <= MaxBound.x;
            bool yCheck;
            yCheck = spawnPos.y >= MinBound.y && spawnPos.y <= MaxBound.y;
            bool sunCheck = DH.TotalPlants <= SD.MaxPlantLifeSupported;
            //Make sure plant is spawned in bounds and doesn't.. 
            //..exceed plant pop limit
            if(xCheck && yCheck && sunCheck)
            {
                Quaternion rot = AD.transform.rotation;
                GameObject a; 
                a = GameObject.Instantiate(AD.OffSpringPrefab, spawnPos, rot);
                AnimalData ad = a.GetComponent<AnimalData>();
                if (ad != null)
                    ad.GD_Inherited = AD.GD_Current;
            }
               
            AD.MateTimer.Value = 0;
            AD.CurrentMate.Value = AD.MaxMate.Value;
            DebugText.text = "Idle";
            returnState = ReturnStates.SUCCESS;
            AD.isMating = false;
        }
        return ReturnState;
    }
}

