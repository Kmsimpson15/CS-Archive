/*******************************************************************************
File: CoyoteBehavior.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Holds behavior tree for coyote AI
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoyoteBehavior : MonoBehaviour
{
    public GeneticData GD_Default;
    public AnimalData AD;
    public Node RootNode;
    public Node Temp;
    // Start is called before the first frame update
    void Start()
    {
        //Initalize Animal Data
        AD.InitalizeAnimalData();
        if (AD.GD_Inherited == null)
            AD.GD_Inherited = GD_Default;
        AD.SetUpGeneticStats();
        TextMeshPro p = AD.NodeText;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Transform t = transform;
        FloatReference z = AD.RefZero;
        FloatReference a = AD.AttackSpeed;
        FloatReference r = AD.RoamSpeed;
        FloatReference f = AD.FleeSpeed;
        TransformReference ca = AD.CurrentAttacker;
        TransformReference npd = AD.NearestPreditor;
        TransformReference nf = AD.NearestPreditor;
        TransformReference npr = AD.NearestPreditor;
        TransformReference nm = AD.NearestPreditor;
        EqualSigns es = EqualSigns.GreaterThan;
        //Create Leaf Nodes
        CheckForDeath healthCheck = new CheckForDeath(AD, p, "Dead");
        CompareFloat calCheck = new CompareFloat(AD.Calories, z, es);
        DestroyObject destroyNode = new DestroyObject(gameObject);

        UpdateAnimalData statUpdate = new UpdateAnimalData(AD, false);

        CheckTransformForNull predCheck = new CheckTransformForNull(npd);
        FleeTransform fleePred = new FleeTransform(npd, t, rb, f, p, "Flee");

        CheckTransformForNull attackerCheck = new CheckTransformForNull(ca);
        Attack attackAttacker = new Attack(AD, p, "Attacking", false);
        SeekTransform seekAttacker;
        seekAttacker = new SeekTransform(ca, t, rb, a, p, "Seek Attacker");

        CompareFloat hungerCheck = new CompareFloat(AD.CurrentHunger, z, es);
        CheckTransformForNull foodCheck = new CheckTransformForNull(nf);
        ConsumeCalories eatNode = new ConsumeCalories(AD, p, "Eat");
        SeekTransform Seekfood;
        Seekfood = new SeekTransform(nf, t, rb, a, p, "Seek Food");

        CompareFloat hungerCheck2 = new CompareFloat(AD.CurrentHunger, z, es);
        CheckTransformForNull foodCheck2 = new CheckTransformForNull(nf);
        CheckTransformForNull preyCheck = new CheckTransformForNull(npr);
        Attack attackPrey = new Attack(AD, p, "Attacking", true);
        SeekTransform seekPrey;
        seekPrey = new SeekTransform(npr, t, rb, a, p, "Seek Prey");

        CompareFloat sleepCheck = new CompareFloat(AD.CurrentSleep, z, es);
        Sleep sleepNode = new Sleep(AD, p, "Sleep");

        CompareFloat mateCheck = new CompareFloat(AD.CurrentMate, z, es);
        CheckTransformForNull nearMateCheck = new CheckTransformForNull(nm);
        Mate mateNode = new Mate(AD, p, "Mate");
        SeekTransform seekMate;
        seekMate = new SeekTransform(nm, t, rb, r, p, "Seek Mate");

        SeekPoint seekRoam = new SeekPoint(AD.RoamPoint, t, rb, r, p, "Roam");



        //Create Flow Control Nodes
        InverterNode deathInvert = new InverterNode(healthCheck);
        List<Node> nL1 = new List<Node>();
        nL1.Add(calCheck);
        nL1.Add(destroyNode);
        SelectorNode calDestroySelector = new SelectorNode(nL1);
        AlwaysFailureNode deathFail = new AlwaysFailureNode(calDestroySelector);
        List<Node> nL2 = new List<Node>();
        nL2.Add(deathInvert);
        nL2.Add(deathFail);
        SelectorNode healthSelector = new SelectorNode(nL2);

        List<Node> nL3 = new List<Node>();
        nL3.Add(predCheck);
        nL3.Add(fleePred);
        SelectorNode fleeSelector = new SelectorNode(nL3);

        List<Node> nL4 = new List<Node>();
        nL4.Add(attackerCheck);
        nL4.Add(attackAttacker);
        nL4.Add(seekAttacker);
        SelectorNode defendSelector = new SelectorNode(nL4);
        List<Node> nL5 = new List<Node>();
        nL5.Add(defendSelector);
        SelectorNode defenceSelector = new SelectorNode(nL5);

        List<Node> nL6 = new List<Node>();
        nL6.Add(foodCheck);
        nL6.Add(eatNode);
        nL6.Add(Seekfood);
        SelectorNode eatSelector = new SelectorNode(nL6);
        List<Node> nL7 = new List<Node>();
        nL7.Add(hungerCheck);
        nL7.Add(eatSelector);
        SelectorNode hungerSelector = new SelectorNode(nL7);

        InverterNode foodCheckInvert = new InverterNode(foodCheck2);
        List<Node> nL8 = new List<Node>();
        nL8.Add(foodCheckInvert);
        nL8.Add(preyCheck);
        nL8.Add(attackPrey);
        nL8.Add(seekPrey);
        SelectorNode huntSelector = new SelectorNode(nL8);
        List<Node> nL9 = new List<Node>();
        nL9.Add(hungerCheck2);
        nL9.Add(huntSelector);
        SelectorNode preySelector = new SelectorNode(nL9);

        List<Node> nL10 = new List<Node>();
        nL10.Add(sleepCheck);
        nL10.Add(sleepNode);
        SelectorNode sleepSelector = new SelectorNode(nL10);

        List<Node> nL11 = new List<Node>();
        nL11.Add(nearMateCheck);
        nL11.Add(mateNode);
        nL11.Add(seekMate);
        SelectorNode reproduceSelector = new SelectorNode(nL11);
        List<Node> nL12 = new List<Node>();
        nL12.Add(mateCheck);
        nL12.Add(reproduceSelector);
        SelectorNode mateSelector = new SelectorNode(nL12);

        List<Node> nL13 = new List<Node>();
        nL13.Add(healthSelector);
        nL13.Add(statUpdate);
        nL13.Add(fleeSelector);
        nL13.Add(defenceSelector);
        nL13.Add(preySelector);
        nL13.Add(mateSelector);
        nL13.Add(hungerSelector);
        nL13.Add(sleepSelector);
        nL13.Add(seekRoam);
        SequenceNode aliveSequence = new SequenceNode(nL13);

        List<Node> nL14 = new List<Node>();
        nL14.Add(aliveSequence);
        SequenceNode rootSequence = new SequenceNode(nL14);

        RootNode = rootSequence;
    }

    // Update is called once per frame
    void Update()
    {
        RootNode.Evaluate();
    }

    
}
