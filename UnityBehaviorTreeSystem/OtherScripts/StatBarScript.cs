/*******************************************************************************
File:      StatbarScript.cs
Author:    Kaelan Simpson
DP Email:  kaelan.simpson@digipen.edu
Date:      4/10/2021
Course:    CS199
Section:   A

Description:
    Updates health/hunger/calories bars for visual demo purposes.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBarScript : MonoBehaviour
{
    public Transform HealthBarFill;
    public Transform FoodBarFill;
    public Transform CalorieBarFill;
    public AnimalData AD;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(HealthBarFill != null)
        {
            float hFill = AD.CurrentHealth.Value / AD.MaxHealth.Value;
            hFill = Mathf.Clamp(hFill, 0, 1);
            HealthBarFill.localScale = new Vector3(hFill, 1, 1);
        }
        if(FoodBarFill != null)
        {
            float fFill = AD.CurrentHunger.Value / AD.MaxHunger.Value;
            fFill = Mathf.Clamp(fFill, 0, 1);
            FoodBarFill.localScale = new Vector3(fFill, 1, 1);
        }
        if(CalorieBarFill != null)
        {
            float cFill = AD.Calories.Value / AD.MaxHealth.Value;
            cFill = Mathf.Clamp(cFill, 0, 1);
            CalorieBarFill.localScale = new Vector3(cFill, 1, 1);
        }
    }
}
