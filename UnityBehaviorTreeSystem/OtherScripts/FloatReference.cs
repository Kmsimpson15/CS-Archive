/*******************************************************************************
File:      FloatReference.cs
Author:    Kaelan Simpson
DP Email:  kaelan.simpson@digipen.edu
Date:      4/10/2021
Course:    CS199
Section:   A

Description:
    Wrapper for a float varible, effectivtly meant to give some of the
    functionalty that pointer in C++ has.  Allows a reference to be stored
    outside of a function (unlike ref and out)
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reference Varibles/Float Reference")]
public class FloatReference : ScriptableObject
{
    public float Value;
}
