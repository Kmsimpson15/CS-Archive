/*******************************************************************************
File:      HelperFunction.cs
Author:    Kaelan Simpson
DP Email:  kaelan.simpson@digipen.edu
Date:      4/10/2021
Course:    CS199
Section:   A

Description:
    A mixture of functions used for pratical purposes throughout other 
    scripts.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SteeringFunctions
{
    //Move towards a point
    public static void Seek(this Rigidbody2D rb, Vector2 current,
                            Vector2 target, float speed)
    {
        //Desired
        Vector2 des = (target - current).normalized * speed;
        //Steer
        Vector2 s = Vector2.ClampMagnitude((des - rb.velocity), speed);
        rb.AddForce(s);
    }

    //Move away from a point
    public static void Flee(this Rigidbody2D rb, Vector2 current,
                            Vector2 target, float speed)
    {
        //Desired
        Vector2 des = (current - target).normalized * speed;
        //Steer
        Vector2 s = Vector2.ClampMagnitude((des - rb.velocity), speed);
        rb.AddForce(s);
    }
}

public static class ExtraMathFunctions
{
    //Takes value from one float reference and gives it to the other
    public static void Absorb(this FloatReference a, FloatReference b, 
                                                                float r)
    {
        float v = Mathf.Clamp(b.Value, 0, r);
        a.Value += v;
        b.Value -= v;
    }

    //Returns a random value within a rnage of a base value
    public static float RandomWithVariation(this float a, float v, float min, 
                                                                    float max)
    {
        float r = Random.Range(a - v, a + v);
        r = Mathf.Clamp(r, min, max);
        return r;
    }

    //Returns the greater of two floats
    public static float ReturnGreater(this float a, float b)
    {
        if (a >= b)
            return a;
        else
            return b;
    }

    //returns the closest transform to the orgin
    public static Transform ReturnClosest(this Transform orgin, Transform t1, 
                                                                Transform t2)
    {
        Transform closest = t1;
        float d1 = Vector2.Distance(orgin.position, t1.position);
        float d2 = Vector2.Distance(orgin.position, t2.position);
        if (d1 > d2)
            closest = t2;
        return closest;
    }
}


