﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundableCamera : MonoBehaviour
{
    public Camera cam;

    public Collider2D TESTstartRoom;
    public Collider2D TESTroomBounds1;
    public Collider2D TESTroomBounds2;
    public Collider2D TESTlargeRoomBounds;
    public Transform TESTplayerTransform;

    private Collider2D cameraBounds;
    private Transform trackedTransform;

    private Vector2 desiredCameraPosition;

    private bool isInLargeRoom = false;
    private bool shouldLerp = false;
    private Vector2 smoothVelocity;
    private float lerpLeeway = 0.001f;
    private float lerpTime = .3f;

    private void Start()
    {
        SnapToSmallRoom(TESTstartRoom);
    }

    private void SetSmallRoomBools(Collider2D boundsCollider)
    {
        isInLargeRoom = false;
        cameraBounds = boundsCollider;
        trackedTransform = null;
    }

    private void SnapToSmallRoom(Collider2D boundsCollider)
    {
        SetSmallRoomBools(boundsCollider);

        transform.position = boundsCollider.bounds.center;
    }

    private void LerpToSmallRoom(Collider2D boundsCollider)
    {
        SetSmallRoomBools(boundsCollider);

        StartLerp();
    }

    private void MoveToLargeRoom(Collider2D boundsCollider, Transform trackedObject)
    {
        isInLargeRoom = true;
        cameraBounds = boundsCollider;
        trackedTransform = trackedObject;

        StartLerp();
    }

    private void StartLerp()
    {
        shouldLerp = true;
        smoothVelocity = Vector3.zero;

    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            print("Lerping camera to room location 1");
            LerpToSmallRoom(TESTroomBounds1);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            print("Lerping camera to room location 2");
            LerpToSmallRoom(TESTroomBounds2);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            print("Lerping camera to TOP room");
            MoveToLargeRoom(TESTlargeRoomBounds, TESTplayerTransform);
        }

        SetDesiredCameraPosition(); 
        
        if(isInLargeRoom)
        {
            ConstrainCameraToBounds(ref desiredCameraPosition);
        }

        if (shouldLerp)
        {
            LerpToNewTrackingLocation();
        }
        else if(isInLargeRoom)
        {
            KeepCameraOnTarget();
        }
        print(desiredCameraPosition);

    }

    private void SetDesiredCameraPosition()
    {
        if(!isInLargeRoom )
        {
            desiredCameraPosition = cameraBounds.bounds.center;
            return;
        }
        else
        {
            desiredCameraPosition = trackedTransform.position;
        }

        /*
        Bounds camBounds = cameraBounds.bounds;
        Vector2 roomUpperRight = camBounds.center + camBounds.extents;
        Vector2 roomLowerLeft = camBounds.center - camBounds.extents;

        Vector2 corner = Vector2.zero;
        Vector2 desiredCameraOffset = Vector2.zero;
        bool useOffsetX = false;
        bool useOffsetY = false;

        //Find real world screen size
        Vector2 halfScreenSize = new Vector2();
        Vector2 screenPoint1 = cam.ViewportToWorldPoint(Vector2.zero);
        Vector2 screenPoint2 = cam.ViewportToWorldPoint(Vector2.one);
        halfScreenSize.x = Mathf.Abs(screenPoint1.x - screenPoint2.x);
        halfScreenSize.y = Mathf.Abs(screenPoint1.y - screenPoint2.y);
        halfScreenSize /= 2;

        if (cam.ViewportToWorldPoint(Vector2.zero).y < roomLowerLeft.y)
        {
            useOffsetY = true;
            corner.y = roomLowerLeft.y;
            desiredCameraOffset.y = halfScreenSize.y;
        }
        else if(cam.ViewportToWorldPoint(Vector2.one).y > roomUpperRight.y)
        {
            useOffsetY = true;
            corner.y = roomUpperRight.y;
            desiredCameraOffset.y = -halfScreenSize.y;
        }

        if (cam.ViewportToWorldPoint(Vector2.zero).x < roomLowerLeft.x)
        {
            useOffsetX = true;
            corner.x = roomLowerLeft.x;
            desiredCameraOffset.x = halfScreenSize.x;
        }
        else if(cam.ViewportToWorldPoint(Vector2.one).x > roomUpperRight.x)
        {
            useOffsetX = true;
            corner.x = roomUpperRight.x;
            desiredCameraOffset.x = -halfScreenSize.x;
        }

        //SuperDebugger.DrawPlus(corner, Color.yellow);

        desiredCameraPosition = new Vector2(corner.x + desiredCameraOffset.x, corner.y + desiredCameraOffset.y);

        //Set player position as desired if player in proper range
        
        if(!useOffsetX)
        {
            desiredCameraPosition.x = trackedTransform.position.x;
        }
        if(!useOffsetY)
        {
            desiredCameraPosition.y = trackedTransform.position.y;
        }

        


        SuperDebugger.DrawPlus(desiredCameraPosition, Color.red, 1, .5f);
        */

    }

    private void ConstrainCameraToBounds(ref Vector2 desiredPosition)
    {
        Bounds camBounds = cameraBounds.bounds;
        Vector2 roomUpperRight = camBounds.center + camBounds.extents;
        Vector2 roomLowerLeft = camBounds.center - camBounds.extents;

        Vector2 corner = Vector2.zero;
        Vector2 desiredCameraOffset = Vector2.zero;
        bool useOffsetX = false;
        bool useOffsetY = false;

        //Find real world screen size
        Vector2 halfScreenSize = new Vector2();
        Vector2 screenPoint1 = cam.ViewportToWorldPoint(Vector2.zero);
        Vector2 screenPoint2 = cam.ViewportToWorldPoint(Vector2.one);
        halfScreenSize.x = Mathf.Abs(screenPoint1.x - screenPoint2.x);
        halfScreenSize.y = Mathf.Abs(screenPoint1.y - screenPoint2.y);
        halfScreenSize /= 2;

        if (cam.ViewportToWorldPoint(Vector2.zero).y < roomLowerLeft.y)
        {
            useOffsetY = true;
            corner.y = roomLowerLeft.y;
            desiredCameraOffset.y = halfScreenSize.y;
        }
        else if (cam.ViewportToWorldPoint(Vector2.one).y > roomUpperRight.y)
        {
            useOffsetY = true;
            corner.y = roomUpperRight.y;
            desiredCameraOffset.y = -halfScreenSize.y;
        }

        if (cam.ViewportToWorldPoint(Vector2.zero).x < roomLowerLeft.x)
        {
            useOffsetX = true;
            corner.x = roomLowerLeft.x;
            desiredCameraOffset.x = halfScreenSize.x;
        }
        else if (cam.ViewportToWorldPoint(Vector2.one).x > roomUpperRight.x)
        {
            useOffsetX = true;
            corner.x = roomUpperRight.x;
            desiredCameraOffset.x = -halfScreenSize.x;
        }

        if(useOffsetX)
        {
            desiredPosition.x = corner.x + desiredCameraOffset.x;
        }
        if(useOffsetY)
        {
            desiredPosition.y = corner.y + desiredCameraOffset.y;
        }
    }

    private void LerpToNewTrackingLocation()
    {
        transform.position = Vector2.SmoothDamp((Vector2)transform.position, desiredCameraPosition, ref smoothVelocity, lerpTime);
        if (Vector2.Distance(transform.position, desiredCameraPosition) < lerpLeeway)
        {
            shouldLerp = false;
        }
    }

    private void KeepCameraOnTarget()
    {
        transform.position = desiredCameraPosition;
    }






}
