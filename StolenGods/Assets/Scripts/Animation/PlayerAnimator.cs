using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public SpriteAnimator spriteAnimator;

    public GroundMover groundMover;

    private void FixedUpdate()
    {
        print("Groundmover is moving: " + groundMover.IsMoving);
        if(groundMover.IsMoving)
        {
            SetMovingDirection();
        }
        else
        {
            print("Is setting standing direction");
            SetStandingDirection();
        }
    }

    private void SetStandingDirection()
    {
        if (groundMover.FaceDirection.x != 0)
        {
            if (groundMover.FaceDirection.x == 1)
            {
                spriteAnimator.Play("StandRight");
            }
            else
            {
                spriteAnimator.Play("StandLeft");
            }
        }
        else if (groundMover.FaceDirection == Vector2.up)
        {
            spriteAnimator.Play("StandUp");
        }
        else if (groundMover.FaceDirection == Vector2.down)
        {
            spriteAnimator.Play("StandDown");
        }
    }

    private void SetMovingDirection()
    {
        if (groundMover.FaceDirection.x != 0)
        {
            if (groundMover.FaceDirection.x == 1)
            {
                spriteAnimator.Play("RunRight");
            }
            else
            {
                spriteAnimator.Play("RunLeft");
            }
        }
        else if (groundMover.FaceDirection == Vector2.up)
        {
            spriteAnimator.Play("RunUp");
        }
        else if (groundMover.FaceDirection == Vector2.down)
        {
            spriteAnimator.Play("RunDown");
        }
    }



}
