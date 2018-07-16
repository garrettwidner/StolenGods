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
            SetStandingDirection();
        }
    }

    private void SetStandingDirection()
    {
        if (groundMover.FaceDirection == Vector2.up)
        {
            spriteAnimator.Play("StandUp");
        }
        else if (groundMover.FaceDirection == Vector2.right)
        {
            spriteAnimator.Play("StandRight");
        }
        else if (groundMover.FaceDirection == Vector2.down)
        {
            spriteAnimator.Play("StandDown");
        }
        else if (groundMover.FaceDirection == Vector2.left)
        {
            spriteAnimator.Play("StandLeft");
        }
    }

    private void SetMovingDirection()
    {
        if (groundMover.FaceDirection == Vector2.up)
        {
            spriteAnimator.Play("RunUp");
        }
        else if (groundMover.FaceDirection == Vector2.right)
        {
            spriteAnimator.Play("RunRight");
        }
        else if(groundMover.FaceDirection == Vector2.down)
        {
            spriteAnimator.Play("RunDown");
        }
        else if(groundMover.FaceDirection == Vector2.left)
        {
            spriteAnimator.Play("RunLeft");
        }
    }



}
