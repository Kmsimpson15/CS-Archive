/*******************************************************************************
File: UpdateAnimalData.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Leaf node, updates all sensory data found in AnimalData.cs Including:
NearestPrey
NearestMate
NearestAttacker
NearestFood
NearestPreditor
Always updates consistance animal timers.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAnimalData : Node
{
    private AnimalData AD;
    bool IsPlant = false;
    public UpdateAnimalData(AnimalData ad, bool isPlant)
    {
        AD = ad;
        IsPlant = isPlant;
    }

    //Return first success for if all fail
    public override ReturnStates Evaluate()
    {
        returnState = ReturnStates.FAILURE;
        if (AD == null)
            return ReturnState;
        returnState = ReturnStates.SUCCESS;
        if (IsPlant)
        {
            AD.CurrentMate.Value -= Time.deltaTime;
            return ReturnState;
        }
        Vector2 pos = AD.transform.position;
        float s = AD.SenseRange.Value;
        float f = AD.FearRange.Value;
        Collider2D[] cols = Physics2D.OverlapCircleAll(pos, s);
        Collider2D[] pCols = Physics2D.OverlapCircleAll(pos, f);
        bool preyCheck = false;
        bool preditorCheck = false;
        bool mateCheck = false;
        bool foodCheck = false;
        //Loop through objects in fear range in search of preditors
        foreach(Collider2D c in pCols)
        {
            AnimalData data = c.GetComponent<AnimalData>();
            if (data == null)
                continue;
            if (data.CurrentHealth == null)
                continue;
            if (data.CurrentHealth.Value <= 0)
                continue;
            //Preditors
            for (int i = 0; i < AD.Preditors.Count; ++i)
            {
                //Check if on the preditor list of the given species
                if (data.Type == AD.Preditors[i])
                {
                    preditorCheck = true;
                    if (AD.NearestPreditor.Value == null)
                        AD.NearestPreditor.Value = data.transform;
                    Transform t1 = data.transform;
                    Transform t2 = AD.NearestPreditor.Value;
                    Transform nearest = AD.transform.ReturnClosest(t1, t2);
                    //The closest is now nearest preditor
                    AD.NearestPreditor.Value = nearest;
                }
            }
        }
        //Loop through objects using sense (not fear) range
        foreach (Collider2D c in cols)
        {
            AnimalData data = c.GetComponent<AnimalData>();
            if (data == null)
                continue;
            if (data.CurrentHealth == null)
                continue;
            //If animal is not alive, check or everything other then food
            if (data.CurrentHealth.Value > 0)
            {
                //Prey
                for (int i = 0; i < AD.Prey.Count; ++i)
                {
                    if (data.Type == AD.Prey[i])
                    {
                        preyCheck = true;
                        if (AD.NearestPrey.Value == null)
                            AD.NearestPrey.Value = data.transform;
                        Transform t1 = data.transform;
                        Transform t2 = AD.NearestPrey.Value;
                        Transform near = AD.transform.ReturnClosest(t1, t2);
                        //The closest is now nearest prey
                        AD.NearestPrey.Value = near;
                    }
                }
                //Mating
                bool b = data.CanReproduce == AD.CanReproduce;
                //Check if animal is right species, sex, and seeking mate
                if (data.Type == AD.Type && !b && data.CurrentMate.Value <= 0)
                {                   
                    mateCheck = true;
                    if (AD.NearestMate.Value == null)
                        AD.NearestMate.Value = data.transform;
                    Transform t1 = data.transform;
                    Transform t2 = AD.NearestMate.Value;
                    Transform near = AD.transform.ReturnClosest(t1, t2);
                    //The Closest is now nearest mate
                    AD.NearestMate.Value = near;
                }
            }
            else
            {
                //Food
                for (int i = 0; i < AD.Food.Count; ++i)
                {
                    //Check if on species food list
                    if(data.Type == AD.Food[i])
                    {
                        foodCheck = true;
                        if (AD.NearestFood.Value == null)
                            AD.NearestFood.Value = data.transform;
                        Transform t1 = data.transform;
                        Transform t2 = AD.NearestFood.Value;
                        Transform near = AD.transform.ReturnClosest(t1, t2);
                        //Closest is now nearest food
                        AD.NearestFood.Value = near;
                    }
                }
            }
        }
        //If any of the following where not found, they are set to null
        if (!preyCheck)
            AD.NearestPrey.Value = null;
        if (!preditorCheck)
            AD.NearestPreditor.Value = null;
        if (!mateCheck && !AD.isMating)
            AD.NearestMate.Value = null;
        if (!foodCheck)
            AD.NearestFood.Value = null;
        
        if(AD.CurrentAttacker.Value != null)
        {
            AnimalData aData;
            aData = AD.CurrentAttacker.Value.GetComponent<AnimalData>();
            //If the nearest attacker is dead they are not longer an attacker
            if (aData != null)
                if (aData.CurrentHealth.Value <= 0)
                    AD.CurrentAttacker.Value = null;
        }

        //Increment timers
        AD.CurrentHunger.Value -= Time.deltaTime;
        AD.CurrentMate.Value -= Time.deltaTime;
        AD.CurrentSleep.Value -= Time.deltaTime;
        Vector2 tPos = AD.transform.position;
        float rd = Vector2.Distance(AD.RoamPoint.Value, tPos);
        //If roam position is reached, set new roam position
        float r = AD.RoamRadius;
        Vector2 rPos = AD.RoamOrgin + Random.insideUnitSphere * r;
        if (rd <= AD.RoamRange)
            AD.RoamPoint.Value = rPos;
        return ReturnState;
    }
}

