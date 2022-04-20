/*******************************************************************************
File:      GeneticData.cs
Author:    Kaelan Simpson
DP Email:  kaelan.simpson@digipen.edu
Date:      4/10/2021
Course:    CS199
Section:   A

Description:
    Stores genetic data for AI that is passed down when they reproduce
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Genetics/GeneticData")]
public class GeneticData : ScriptableObject
{
    //Movement
    public FloatReference SpeedVariation;
    public FloatReference RoamSpeed;
    public FloatReference FleeSpeed;
    public FloatReference AttackSpeed;
    //Sleep
    public FloatReference SleepDuration;
    public FloatReference SleepDurationVariation;
    public FloatReference MaxSleep;
    public FloatReference MaxSleepVariation;
    //Mating
    public FloatReference MateDuration;
    public FloatReference MateRange;
    public FloatReference MateRangeVariation;
    public FloatReference MateDurationVariation;
    public FloatReference OffSpring;
    public FloatReference OffSpringVariation;
    public FloatReference MaxMate;
    public FloatReference MaxMateVariation;
    //Attacking
    public FloatReference AttackDamage;
    public FloatReference AttackDamageVariation;
    public FloatReference AttackRange;
    public FloatReference AttackRangeVariation;
    public FloatReference AttackCoolDown;
    public FloatReference AttackCoolDownVariation;
    //Eating
    public FloatReference EatDuration;
    public FloatReference EatDurationVariation;
    public FloatReference EatRange;
    public FloatReference EatRangeVariation;
    public FloatReference MaxHunger;
    public FloatReference MaxHungerVariation;
    //Health
    public FloatReference MaxHealth;
    public FloatReference MaxHealthVariation;
    //Senses
    public FloatReference SenseRange;
    public FloatReference SenseRangeVariation;
    public FloatReference FearRange;
    public FloatReference FearRangeVariation;
    public Species Type;
    public List<Species> Food;
    public List<Species> Prey;
    public List<Species> Preditors;

    //Initailized float the genetic varibles
    public void InitalizeGeneticData()
    {
        string s  = "FloatReference";
        RoamSpeed = (FloatReference)ScriptableObject.CreateInstance(s);
        FleeSpeed = (FloatReference)ScriptableObject.CreateInstance(s);
        AttackSpeed = (FloatReference)ScriptableObject.CreateInstance(s);
        SpeedVariation = (FloatReference)ScriptableObject.CreateInstance(s);
        SleepDuration = (FloatReference)ScriptableObject.CreateInstance(s);
        ScriptableObject so1;
        so1 = ScriptableObject.CreateInstance(s);
        SleepDurationVariation = (FloatReference)so1;
        MaxSleep = (FloatReference)ScriptableObject.CreateInstance(s);
        ScriptableObject so2 = ScriptableObject.CreateInstance(s);
        MaxSleepVariation = (FloatReference)so2;
        MateDuration = (FloatReference)ScriptableObject.CreateInstance(s);
        ScriptableObject so3 = ScriptableObject.CreateInstance(s);
        MateDurationVariation = (FloatReference)so3;
        OffSpring = (FloatReference)ScriptableObject.CreateInstance(s);
        ScriptableObject so4 = ScriptableObject.CreateInstance(s);
        OffSpringVariation = (FloatReference)so4;
        MaxMate = (FloatReference)ScriptableObject.CreateInstance(s);
        MaxMateVariation = (FloatReference)ScriptableObject.CreateInstance(s);
        AttackDamage = (FloatReference)ScriptableObject.CreateInstance(s);
        ScriptableObject so5 = ScriptableObject.CreateInstance(s);
        AttackDamageVariation = (FloatReference)so5;
        AttackRange = (FloatReference)ScriptableObject.CreateInstance(s);
        ScriptableObject so6 = ScriptableObject.CreateInstance(s);
        AttackRangeVariation = (FloatReference)so6;
        AttackCoolDown = (FloatReference)ScriptableObject.CreateInstance(s);
        ScriptableObject so7 = ScriptableObject.CreateInstance(s);
        AttackCoolDownVariation = (FloatReference)so7;
        ScriptableObject so8 = ScriptableObject.CreateInstance(s);
        AttackRangeVariation = (FloatReference)so8;
        EatDuration = (FloatReference)ScriptableObject.CreateInstance(s);
        ScriptableObject so9 = ScriptableObject.CreateInstance(s);
        EatDurationVariation = (FloatReference)so9;
        EatRange = (FloatReference)ScriptableObject.CreateInstance(s);
        ScriptableObject so10 = ScriptableObject.CreateInstance(s);
        EatRangeVariation = (FloatReference)so10;
        MaxHunger = (FloatReference)ScriptableObject.CreateInstance(s);
        ScriptableObject so11 = ScriptableObject.CreateInstance(s);
        MaxHungerVariation = (FloatReference)so11;
        MaxHealth = (FloatReference)ScriptableObject.CreateInstance(s);
        ScriptableObject so12 = ScriptableObject.CreateInstance(s);
        MaxHealthVariation = (FloatReference)so12;
        SenseRange = (FloatReference)ScriptableObject.CreateInstance(s);
        ScriptableObject so13 = ScriptableObject.CreateInstance(s);
        SenseRangeVariation = (FloatReference)so13;
        FearRange = (FloatReference)ScriptableObject.CreateInstance(s);
        ScriptableObject so14 = ScriptableObject.CreateInstance(s);
        FearRangeVariation = (FloatReference)so14;
        MateRange = (FloatReference)ScriptableObject.CreateInstance(s);
        ScriptableObject so15 = ScriptableObject.CreateInstance(s);
        MateRangeVariation = (FloatReference)so15;
    }
}
