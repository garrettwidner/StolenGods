using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundableCamera : MonoBehaviour
{
    public Collider2D TESTroomBounds1;
    public Collider2D TESTroomBounds2;

    private Collider2D cameraBounds;
    private Transform trackedTransform;

    private bool isInLargeRoom = false;
    private bool shouldLerp = false;
    private Vector2 smoothVelocity;
    private float lerpLeeway = 0.001f;
    private float lerpTime = .3f;

    private void MoveToSmallRoom(Collider2D boundsCollider)
    {
        isInLargeRoom = false;
        cameraBounds = boundsCollider;
        trackedTransform = boundsCollider.transform;

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

    private void LerpToNewTrackingLocation()
    {
        transform.position = Vector2.SmoothDamp((Vector2)transform.position, trackedTransform.position, ref smoothVelocity, lerpTime);
        if(Vector2.Distance(transform.position, trackedTransform.position) < lerpLeeway)
        {
            shouldLerp = false;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            print("Lerping camera to room location 1");
            MoveToSmallRoom(TESTroomBounds1);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            print("Lerping camera to room location 2");
            MoveToSmallRoom(TESTroomBounds2);
        }

        if (shouldLerp)
        {
            LerpToNewTrackingLocation();
        }
    }

    //Just make the camera not be able to go outside the bounds of a bounding box. Otherwise, let it follow the character.
    //Add functions to be able to call cam to snap unmoving to inside of room, change rooms by lerping, and change to a 
    //bounded room


}
