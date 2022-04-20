/*******************************************************************************
File:      DisplayHUD.cs
Author:    Kaelan Simpson
DP Email:  kaelan.simpson@digipen.edu
Date:      4/10/2021
Course:    CS199
Section:   A

Description:
    Updates HUD text info relavant to demo.  Also contains functions for 
    adding/removing AI from the scene during demo play.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHUD : MonoBehaviour
{
    public TextMeshProUGUI Deer;
    public TextMeshProUGUI Bear;
    public TextMeshProUGUI Moose;
    public TextMeshProUGUI Rabbit;
    public TextMeshProUGUI Coyote;
    public TextMeshProUGUI BerryBush;
    public TextMeshProUGUI TallGrass;
    public GameObject ExpandedPanel1;
    public GameObject MinmizedPanel1;
    public GameObject ExpandedPanel2;
    public GameObject MinmizedPanel2;
    public int TotalPlants;
    private bool Expanded = true;
    private Vector2 Orgin = Vector2.zero;
    private float Radius = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        ExpandedPanel1.SetActive(true);
        MinmizedPanel1.SetActive(false);
        ExpandedPanel2.SetActive(true);
        MinmizedPanel2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Toggle HUD
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!Expanded)
            {
                Expanded = true;
                ExpandedPanel1.SetActive(true);
                MinmizedPanel1.SetActive(false);
                ExpandedPanel2.SetActive(true);
                MinmizedPanel2.SetActive(false);
            }
            else
            {
                Expanded = false;
                ExpandedPanel1.SetActive(false);
                MinmizedPanel1.SetActive(true);
                ExpandedPanel2.SetActive(false);
                MinmizedPanel2.SetActive(true);
            }
        }
        int BearCountM = 0;
        int BearCountF = 0;
        int DeerCountM = 0;
        int DeerCountF = 0;
        int MooseCountM = 0;
        int MooseCountF = 0;
        int CoyoteCountM = 0;
        int CoyoteCountF = 0;
        int RabbitCountM = 0;
        int RabbitCountF = 0;
        int BerryBushesCount = 0;
        int tallGrassCount = 0;
        AnimalData[] animals = FindObjectsOfType<AnimalData>();
        foreach(AnimalData ad in animals)
        {
            switch (ad.Type)
            {
                case Species.DEER:
                    if (ad.CanReproduce)
                        ++DeerCountF;
                    else
                        ++DeerCountM;
                    break;
                case Species.MOOSE:
                    if (ad.CanReproduce)
                        ++MooseCountF;
                    else
                        ++MooseCountM;
                    break;
                case Species.BEAR:
                    if (ad.CanReproduce)
                        ++BearCountF;
                    else
                        ++BearCountM;
                    break;
                case Species.COYOTE:
                    if (ad.CanReproduce)
                        ++CoyoteCountF;
                    else
                        ++CoyoteCountM;
                    break;
                case Species.RABBIT:
                    if (ad.CanReproduce)
                        ++RabbitCountF;
                    else
                        ++RabbitCountM;
                    break;
                case Species.BERRYBUSH:
                    ++BerryBushesCount;
                    break;
                case Species.TALLGRASS:
                    ++tallGrassCount;
                    break;
                default:
                    //Nothing
                    break;
            }
        }
        //Set info text
        TotalPlants = tallGrassCount + BerryBushesCount;
        Deer.text = SetPopText("Deer: ", DeerCountM, DeerCountF, false);
        Bear.text = SetPopText("Bear: ", BearCountM, BearCountF, false);
        Moose.text = SetPopText("Moose: ", MooseCountM, MooseCountF, false);
        string s1 = SetPopText("Rabbit: ", RabbitCountM, RabbitCountF, false);
        Rabbit.text = s1;
        string s2 = SetPopText("Coyote: ", CoyoteCountM, CoyoteCountF, false);
        Coyote.text = s2;
        string s3 = SetPopText("Berry Bush: ", BerryBushesCount, 0, true);
        BerryBush.text = s3;
        TallGrass.text = SetPopText("Tall Grass: ", tallGrassCount, 0, true);
    }
    private string SetPopText(string a, int m, int f, bool isPlant)
    {
        string r = "";
        if (isPlant)
            r = a + m;
        else
        {
            r = a + (m + f);
            r += "\n";
            r += "M: " + m;
            r += "\n";
            r += "F: " + f;
        }
        return r;
    }

    //Spawns a female Ai
    public void SpawnFemaleAI(GameObject obj)
    {
        Vector2 pos = Orgin + (Vector2)Random.insideUnitSphere * Radius;
        Quaternion rot = Quaternion.identity;
        GameObject a = Instantiate(obj, pos, rot);
        AnimalData ad = a.GetComponent<AnimalData>();
        if (ad != null)
            ad.ensureFemale = true;
    }

    //Spawns male AI
    public void SpawnMaleAI(GameObject obj)
    {
        Vector2 pos = Orgin + (Vector2)Random.insideUnitSphere * Radius;
        Quaternion rot = Quaternion.identity;
        GameObject a = Instantiate(obj, pos, rot);
        AnimalData ad = a.GetComponent<AnimalData>();
        if (ad != null)
            ad.ensureMale = true;
    }

    //Removes female AI
    public void RemoveFemaleAI(int type)
    {
        AnimalData[] animals = FindObjectsOfType<AnimalData>();
        foreach (AnimalData ad in animals)
        {
            if(ad.Type == (Species)type && ad.CanReproduce)
            {
                Destroy(ad.gameObject);
                return;
            }
        }
    }

    //Removes male AI
    public void RemoveMaleAI(int type)
    {
        AnimalData[] animals = FindObjectsOfType<AnimalData>();
        foreach (AnimalData ad in animals)
        {
            if (ad.Type == (Species)type && !ad.CanReproduce)
            {
                Destroy(ad.gameObject);
                return;
            }
        }
    }
}