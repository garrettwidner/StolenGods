using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    public GroundMover groundMover;
    private Vector2 velocity;

    private void Start()
    {
        velocity = Vector2.zero;
    }

    private void Update()
    {
        velocity = Vector2.zero;
        groundMover.Move(ref velocity);


    }

}
