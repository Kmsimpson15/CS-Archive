/*******************************************************************************
File:      SelectionScript.cs
Author:    Kaelan Simpson
DP Email:  kaelan.simpson@digipen.edu
Date:      4/10/2021
Course:    CS199
Section:   A

Description:
    Manages selection of AI during demo play.  Activates a highlight and sets
    the current selection on the camera.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScript : MonoBehaviour
{
    public Color NormalColor;
    public Color HighLightedColor;
    public SpriteRenderer EffectedSprite;
    private bool MouseOver = false;
    private CameraController CamControls;
    private void Start()
    {
        CamControls = FindObjectOfType<CameraController>();
        EffectedSprite.color = NormalColor;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!MouseOver)
            {
                EffectedSprite.color = NormalColor;
                if (CamControls.CurrentSelection == transform)
                    CamControls.CurrentSelection = null;
            }
        }
    }
    private void OnMouseDown()
    {
        EffectedSprite.color = HighLightedColor;
        CamControls.CurrentSelection = transform;
    }
    private void OnMouseEnter()
    {
        MouseOver = true;
    }

    private void OnMouseExit()
    {
        MouseOver = false;
    }
}
