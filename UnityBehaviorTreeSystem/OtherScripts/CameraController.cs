/*******************************************************************************
File: CameraController.cs
Author: Kaelan Simpson
DP Email: kaelan.simpson@digipen.edu
Date: 4/10/2020
Course: CS199
Section: A
Description:
Top down camera controller, allows panning with screen edges, movement
WASD/Arrow keys, and zooming with the mouse scroll wheel.  Camera is bound to 
the boarders of the level.
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{

    [Header("Camera Movement")]
    public float MoveSpeed = 2;
    private Vector2 TargetPos = Vector2.zero;

    [Header("Camera Zoom")]
    public float ZoomSensitivityModifier = 1;
    public float ZoomSensitivity = 60;
    public float MinZoom = 3;
    public float MaxZoom = 30;
    private float Zoom = 8;

    [Header("General")]
    [Range(0f, 1.0f)]
    public float Interpolant = 0.15f;
    public Vector2 MapSize = new Vector2(60f, 60f);
    public float EdgePanPixelMargin = 15f;

    [Header("External References")]
    public Transform MovePivotTransform;
    public Transform YawPivotTransform;
    public Transform PitchPivotTransform;

    [HideInInspector]
    public Transform CurrentSelection;

    private bool follow = false;
    private Camera Cam;
    // Start is called before the first frame update
    void Start()
    {
        Cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Demo Debug Keys
        if(Input.GetKeyDown(KeyCode.Q))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.F))
            FocusOnSelection();
        if (Input.GetKeyDown(KeyCode.Alpha0))
            Time.timeScale =0;
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Time.timeScale = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Time.timeScale = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Time.timeScale = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            Time.timeScale = 4;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            Time.timeScale = 5;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            Time.timeScale = 6;
        if (Input.GetKeyDown(KeyCode.Alpha7))
            Time.timeScale = 7;
        if (Input.GetKeyDown(KeyCode.Alpha8))
            Time.timeScale = 8;
        if (Input.GetKeyDown(KeyCode.Alpha9))
            Time.timeScale = 9;
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if(CurrentSelection != null)
        {
            AnimalData ad = CurrentSelection.GetComponent<AnimalData>();
            //Debug Keys for Selected Ai
            if (ad != null)
            {
                if (Input.GetKeyDown(KeyCode.V))
                    ad.CurrentHealth.Value = 0;
                if (Input.GetKeyDown(KeyCode.B))
                    ad.CurrentHunger.Value = 0;
                if (Input.GetKeyDown(KeyCode.N))
                    ad.CurrentMate.Value = 0;
                if (Input.GetKeyDown(KeyCode.M))
                    ad.CurrentSleep.Value = 0;
            }
        }


        CameraZoom();
        if (follow)
            FollowTransform();
        else
        {
            KeyboardCameraPan();
            MouseEdgeCameraPan();
            CameraMove();
        }
    }

    //Pan camera with WASD or arrow keys
    private void KeyboardCameraPan()
    {
        Vector2 dir = Vector2.zero;
        //Move up
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            dir += (Vector2)YawPivotTransform.up;

        //Move left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            dir -= (Vector2)YawPivotTransform.right;

        //Move down
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            dir -= (Vector2)YawPivotTransform.up;
        //Move right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            dir += (Vector2)YawPivotTransform.right;
        //Normalize and apply speed
        TargetPos += dir.normalized * MoveSpeed * Time.deltaTime;
        //Lerp pan movement
        Vector2 pivotPos = MovePivotTransform.position;
        Vector2 pos = Vector2.Lerp(pivotPos, TargetPos, Interpolant);
        float z = MovePivotTransform.position.z;
        Vector3 pos2 = new Vector3(pos.x, pos.y, z);
        MovePivotTransform.position = pos2;
    }

    //Follow selected object
    private void FollowTransform()
    {
        if(CurrentSelection == null)
        {
            follow = false;
            return;
        }
        //Follow AI
        Vector2 pivotPos = MovePivotTransform.position;
        Vector2 p = CurrentSelection.position;
        Vector2 pos = Vector2.Lerp(pivotPos, p, Interpolant);
        float z = MovePivotTransform.position.z;
        Vector3 pos2 = new Vector3(pos.x, pos.y, z);
        MovePivotTransform.position = pos2;
    }

    //Pan camera based on moving the mouse against the edges of the screen
    private void MouseEdgeCameraPan()
    {
        Vector2 dir = Vector2.zero;

        //Mouse left margin
        bool posCheck = Input.mousePosition.x >= 0;
        bool maCheck = Input.mousePosition.x <= EdgePanPixelMargin;
        if (posCheck && maCheck)
            dir -= (Vector2)YawPivotTransform.right;

        //Mouse on left margin
        posCheck = Input.mousePosition.x <= Screen.width;
        maCheck = Input.mousePosition.x >= Screen.width - EdgePanPixelMargin;
        if (posCheck && maCheck)
            dir += (Vector2)YawPivotTransform.right;

        //Mouse on top margin
        posCheck = Input.mousePosition.y <= Screen.height;
        maCheck = Input.mousePosition.y >= Screen.height - EdgePanPixelMargin;
        if (posCheck && maCheck)
            dir += (Vector2)YawPivotTransform.up;

        //Mouse on bottom margin
        posCheck = Input.mousePosition.y >= 0;
        maCheck = Input.mousePosition.y <= EdgePanPixelMargin;
        if (posCheck && maCheck)
            dir -= (Vector2)YawPivotTransform.up;

        TargetPos += dir.normalized * MoveSpeed * Time.deltaTime;
    }

    //Move camera in general
    private void CameraMove()
    {
        //Clamp movement to level bounds
        float x = Mathf.Clamp(TargetPos.x, -MapSize.x / 2, MapSize.x / 2);
        float y = Mathf.Clamp(TargetPos.y, -MapSize.y / 2, MapSize.y / 2);
        TargetPos = new Vector2(x, y);
        Vector2 pivotPos = MovePivotTransform.position;
        Vector2 pos = Vector2.Lerp(pivotPos, TargetPos, Interpolant);
        float z = MovePivotTransform.position.z;
        Vector3 pos2 = new Vector3(pos.x, pos.y, z);
        MovePivotTransform.position = pos2;
    }

    //Zooms camera by changing its local position on the z axis
    public void CameraZoom()
    {
        //Apply settings modifier to sensitivity
        float s = ZoomSensitivity * ZoomSensitivityModifier;
        Zoom -= Input.mouseScrollDelta.y * s * Time.deltaTime;
        //Clamp zoom to min and max values
        Zoom = Mathf.Clamp(Zoom, MinZoom, MaxZoom);
        Cam.orthographicSize = Zoom;
    }

    public void FocusOnSelection()
    {
        if (CurrentSelection == null)
            return;
        Zoom = MinZoom;
        float x = CurrentSelection.position.x;
        float y = CurrentSelection.position.y;
        float z = MovePivotTransform.position.z;
        MovePivotTransform.position = new Vector3(x, y, z);
        follow = true;
    }
}
