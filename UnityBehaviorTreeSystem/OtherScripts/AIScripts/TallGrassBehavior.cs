/*******************************************************************************
File: TallGrassbehavior.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Holds behavior tree for tall grass AI
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TallGrassBehavior : MonoBehaviour
{
    public GeneticData GD_Default;
    public AnimalData AD;
    public Node RootNode;
    public Node Temp;
    private Vector2 MaxBounds = new Vector2(30.0f, 30.0f);
    private Vector2 MinBounds = new Vector2(-30.0f, -30.0f);
    // Start is called before the first frame update
    void Start()
    {
        //Initalize Animal Data
        AD.InitalizeAnimalData();
        if (AD.GD_Inherited == null)
            AD.GD_Inherited = GD_Default;
        AD.SetUpGeneticStats();

        TextMeshPro p = AD.NodeText;
        FloatReference z = AD.RefZero;
        EqualSigns es = EqualSigns.GreaterThan;

        //Create Leaf Nodes
        CompareFloat calCheck = new CompareFloat(AD.Calories, AD.RefZero, es);
        DestroyObject destroyNode = new DestroyObject(gameObject);

        UpdateAnimalData statUpdate = new UpdateAnimalData(AD, true);

        CompareFloat mateCheck = new CompareFloat(AD.CurrentMate, z, es);
        PlantMate mateNode = new PlantMate(AD, p, MaxBounds, MinBounds);

        //Create Flow Control Nodes
        List<Node> nL1 = new List<Node>();
        nL1.Add(calCheck);
        nL1.Add(destroyNode);
        SelectorNode calDestroySelector = new SelectorNode(nL1);

        List<Node> nL2 = new List<Node>();
        nL2.Add(mateCheck);
        nL2.Add(mateNode);
        SelectorNode mateSelector = new SelectorNode(nL2);

        List<Node> nL3 = new List<Node>();
        nL3.Add(statUpdate);
        nL3.Add(mateSelector);
        SequenceNode mateSequencer = new SequenceNode(nL3);

        List<Node> nL4 = new List<Node>();
        nL4.Add(calDestroySelector);
        nL4.Add(mateSequencer);
        SequenceNode rootSequence = new SequenceNode(nL4);

        RootNode = rootSequence;
    }

    // Update is called once per frame
    void Update()
    {
        RootNode.Evaluate();
    }

    
}
