/*******************************************************************************
File:      AnimalData.cs
Author:    Kaelan Simpson
DP Email:  kaelan.simpson@digipen.edu
Date:      4/10/2021
Course:    CS199
Section:   A

Description:
    Stores game data for AI, data is intially set using genetic data that it
    inherits.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Species {DEER, BEAR, BERRYBUSH, COYOTE, RABBIT, MOOSE, TALLGRASS}
public class AnimalData : MonoBehaviour
{
    //Movement
    public float RoamRadius = 30.0f;
    public float RoamRange = 0.5f;
    public Vector3 RoamOrgin = Vector3.zero;
    public FloatReference RoamSpeed;
    public FloatReference FleeSpeed;
    public FloatReference AttackSpeed;
    public Vector2Reference RoamPoint;
    //Sleep
    public FloatReference SleepDuration;
    public FloatReference SleepTimer;
    public FloatReference MaxSleep;
    public FloatReference CurrentSleep;
    //Mating
    public FloatReference MateDuration;
    public FloatReference MateRange;
    public FloatReference OffSpring;
    public FloatReference MateTimer;
    public FloatReference MaxMate;
    public FloatReference CurrentMate;
    public TransformReference NearestMate;
    public GameObject OffSpringPrefab;
    public bool isMating = false;
    public bool CanReproduce;
    public bool ensureFemale = false;
    public bool ensureMale = false;
    //Attacking
    public FloatReference AttackDamage;
    public FloatReference AttackRange;
    public FloatReference AttackCoolDown;
    public FloatReference AttackTimer;
    public TransformReference NearestPrey;
    public TransformReference NearestPreditor;
    public TransformReference CurrentAttacker;
    //Eating
    public FloatReference EatDuration;
    public FloatReference MaxHunger;
    public FloatReference CurrentHunger;
    public FloatReference EatTimer;
    public FloatReference EatRange;
    public TransformReference NearestFood;
    //Health
    public FloatReference MaxHealth;
    public FloatReference CurrentHealth;
    public FloatReference Calories;
    public bool IsAlive = true;
    //Senses
    public FloatReference SenseRange;
    public FloatReference FearRange;
    //Genetic
    public GeneticData GD_Inherited;
    public GeneticData GD_Current;
    public Species Type;
    public List<Species> Food;
    public List<Species> Prey;
    public List<Species> Preditors;
    //Constant
    public FloatReference RefZero;
    public TextMeshPro NodeText;
    public float DecayRate = 0.5f;



    //Initalizes data
    public void InitalizeAnimalData()
    {
        string s = "FloatReference";
        string v = "Vector2Reference";
        string t = "TransformReference";
        RoamPoint = (Vector2Reference)ScriptableObject.CreateInstance(v);
        RoamSpeed = (FloatReference)ScriptableObject.CreateInstance(s);
        FleeSpeed = (FloatReference)ScriptableObject.CreateInstance(s);
        AttackSpeed = (FloatReference)ScriptableObject.CreateInstance(s);
        SleepDuration = (FloatReference)ScriptableObject.CreateInstance(s);
        MaxSleep = (FloatReference)ScriptableObject.CreateInstance(s);
        CurrentSleep = (FloatReference)ScriptableObject.CreateInstance(s);
        SleepTimer = (FloatReference)ScriptableObject.CreateInstance(s);
        MateRange = (FloatReference)ScriptableObject.CreateInstance(s);
        MateDuration = (FloatReference)ScriptableObject.CreateInstance(s);
        OffSpring = (FloatReference)ScriptableObject.CreateInstance(s);
        MateTimer = (FloatReference)ScriptableObject.CreateInstance(s);
        MaxMate = (FloatReference)ScriptableObject.CreateInstance(s);
        CurrentMate = (FloatReference)ScriptableObject.CreateInstance(s);
        NearestMate = (TransformReference)ScriptableObject.CreateInstance(t);
        AttackDamage = (FloatReference)ScriptableObject.CreateInstance(s);
        AttackRange = (FloatReference)ScriptableObject.CreateInstance(s);
        AttackCoolDown = (FloatReference)ScriptableObject.CreateInstance(s);
        AttackTimer = (FloatReference)ScriptableObject.CreateInstance(s);
        ScriptableObject so1 = ScriptableObject.CreateInstance(t);
        CurrentAttacker = (TransformReference)so1;
        ScriptableObject so2 = ScriptableObject.CreateInstance(t);
        NearestPreditor = (TransformReference)so2;
        NearestPrey = (TransformReference)ScriptableObject.CreateInstance(t);
        EatDuration = (FloatReference)ScriptableObject.CreateInstance(s);
        EatTimer = (FloatReference)ScriptableObject.CreateInstance(s);
        EatRange = (FloatReference)ScriptableObject.CreateInstance(s);
        MaxHunger = (FloatReference)ScriptableObject.CreateInstance(s);
        CurrentHunger = (FloatReference)ScriptableObject.CreateInstance(s);
        NearestFood = (TransformReference)ScriptableObject.CreateInstance(t);
        MaxHealth = (FloatReference)ScriptableObject.CreateInstance(s);
        CurrentHealth = (FloatReference)ScriptableObject.CreateInstance(s);
        Calories = (FloatReference)ScriptableObject.CreateInstance(s);
        SenseRange = (FloatReference)ScriptableObject.CreateInstance(s);
        FearRange = (FloatReference)ScriptableObject.CreateInstance(s);
        RefZero = (FloatReference)ScriptableObject.CreateInstance(s);
        RefZero.Value = 0;
        SleepTimer.Value = 0;
        EatTimer.Value = 0;
        MateTimer.Value = 0;
        AttackTimer.Value = 0;
        isMating = false;
        RoamPoint.Value = RoamOrgin + Random.insideUnitSphere * RoamRadius;
        string g = "GeneticData";
        GD_Current = (GeneticData)ScriptableObject.CreateInstance(g);
        GD_Current.InitalizeGeneticData();
    }

    //Copies inhertied genes to animal data after slightly modifing it..
    //..Saves modified copy to be passed down to offspring
    public void SetUpGeneticStats()
    {
        float m = Mathf.Infinity;
        GeneticData i = GD_Inherited;
        GeneticData c = GD_Current;
        //Asign stats based on inherited genetics
        float f1 = i.SpeedVariation.Value;
        FleeSpeed.Value = i.FleeSpeed.Value.RandomWithVariation(f1, 1, m);
        float speedDif1 = i.RoamSpeed.Value/ FleeSpeed.Value;
        float speedDif2 = i.AttackSpeed.Value / FleeSpeed.Value;
        RoamSpeed.Value = FleeSpeed.Value * speedDif1;
        AttackSpeed.Value = FleeSpeed.Value * speedDif2;
        float f2 = i.SleepDurationVariation.Value;
        f2 = i.SleepDuration.Value.RandomWithVariation(f2, 1, m);
        SleepDuration.Value = f2;
        float f3 = i.MaxSleepVariation.Value;
        f3 = i.MaxSleep.Value.RandomWithVariation(f3, 1, m);
        MaxSleep.Value = f3;
        float f4 = i.MateRangeVariation.Value;
        f4 = i.MateRange.Value.RandomWithVariation(f4, 0, m);
        MateRange.Value = f4;
        float f5 = i.MateDurationVariation.Value;
        f5 = i.MateDuration.Value.RandomWithVariation(f5, 0, m);
        MateDuration.Value = f5;
        float f6 = i.MaxMateVariation.Value;
        f6 = i.MaxMate.Value.RandomWithVariation(f6, 10, m);
        MaxMate.Value = f6;
        float f7 = i.OffSpringVariation.Value;
        f7 = i.OffSpring.Value.RandomWithVariation(f7, 1, m);
        OffSpring.Value = f7;
        float f8 = i.AttackDamageVariation.Value;
        f8 = i.AttackDamage.Value.RandomWithVariation(f8, 0, m);
        AttackDamage.Value = f8;
        float f9 = i.AttackRangeVariation.Value;
        f9 = i.AttackRange.Value.RandomWithVariation(f9, 0, m);
        AttackRange.Value = f9;
        float f10 = i.AttackRangeVariation.Value;
        f10 = i.AttackRange.Value.RandomWithVariation(f10, 0, m);
        AttackCoolDown.Value = f10;
        float f11 = i.EatDurationVariation.Value;
        f11 = i.EatDuration.Value.RandomWithVariation(f11, 0, m);
        EatDuration.Value = f11;
        float f12 = i.EatRangeVariation.Value;
        f12 = i.EatRange.Value.RandomWithVariation(f12, 0, m);
        EatRange.Value = f12;
        float f13 = i.MaxHungerVariation.Value;
        f13 = i.MaxHunger.Value.RandomWithVariation(f13, 1, m);
        MaxHunger.Value = f13;
        float f14 = i.SenseRangeVariation.Value;
        f14 = i.SenseRange.Value.RandomWithVariation(f14, 0, m);
        SenseRange.Value = f14;
        float f15 = i.FearRangeVariation.Value;
        f15 = i.FearRange.Value.RandomWithVariation(f15, 1, m);
        FearRange.Value = f15;
        float f16 = i.MaxHealthVariation.Value;
        f16 = i.MaxHealth.Value.RandomWithVariation(f16, 1, m);
        MaxHealth.Value = f16;
        Calories.Value = MaxHealth.Value;
        CurrentHealth.Value = MaxHealth.Value;
        if (!IsAlive)
            CurrentHealth.Value = 0;
        CurrentHunger.Value = MaxHunger.Value;
        CurrentSleep.Value = MaxSleep.Value;
        CurrentMate.Value = MaxMate.Value;
        Type = i.Type;
        Food = i.Food;
        Prey = i.Prey;
        Preditors = i.Preditors;
        int r = Random.Range(0, 2);
        if (r < 0.5f)
            CanReproduce = true;
        else
            CanReproduce = false;
        if (ensureFemale)
            CanReproduce = true;
        if (ensureMale)
            CanReproduce = false;

        //Copy over gentic code
        c.RoamSpeed.Value = i.RoamSpeed.Value;
        c.FleeSpeed.Value = i.FleeSpeed.Value;
        c.AttackSpeed.Value = i.AttackSpeed.Value;
        c.SpeedVariation.Value = i.SpeedVariation.Value;
        c.SleepDuration.Value = i.SleepDuration.Value;
        c.SleepDurationVariation.Value = i.SleepDurationVariation.Value;
        c.MaxSleep.Value = i.MaxSleep.Value;
        c.MaxSleepVariation = i.MaxSleepVariation;
        c.MateRange.Value = i.MateRange.Value;
        c.MateRangeVariation.Value = i.MateRangeVariation.Value;
        c.MateDuration.Value = i.MateDuration.Value;
        c.MateDurationVariation.Value = i.MateDurationVariation.Value;
        c.MaxMate.Value = i.MaxMate.Value;
        c.MaxMateVariation = i.MaxMateVariation;
        c.OffSpring.Value = i.OffSpring.Value;
        c.OffSpringVariation.Value = i.OffSpringVariation.Value;
        c.AttackDamage.Value = i.AttackDamage.Value;
        c.AttackDamageVariation.Value = i.AttackDamageVariation.Value;
        c.AttackRange.Value = i.AttackRange.Value;
        c.AttackRangeVariation.Value = i.AttackRangeVariation.Value;
        c.AttackCoolDown.Value = i.AttackCoolDown.Value;
        c.AttackCoolDownVariation.Value = i.AttackCoolDownVariation.Value;
        c.EatDuration.Value = i.EatDuration.Value;
        c.EatDurationVariation.Value = i.EatDurationVariation.Value;
        c.EatRange.Value = i.EatRange.Value;
        c.EatRangeVariation.Value = i.EatRangeVariation.Value;
        c.MaxHunger.Value = i.MaxMate.Value;
        c.MaxHungerVariation = i.MaxHungerVariation;
        c.MaxHealth.Value = i.MaxHealth.Value;
        c.MaxHealthVariation.Value = i.MaxHealthVariation.Value;
        c.SenseRange.Value = i.SenseRange.Value;
        c.SenseRangeVariation.Value = i.SenseRangeVariation.Value;
        c.FearRange.Value = i.FearRange.Value;
        c.FearRangeVariation.Value = i.FearRangeVariation.Value;
        c.Type = i.Type;
        c.Food = i.Food;
        c.Prey = i.Prey;
        c.Preditors = i.Preditors;
    }
}
