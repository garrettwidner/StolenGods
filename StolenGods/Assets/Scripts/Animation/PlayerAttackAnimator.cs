using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnimator : MonoBehaviour
{
    [Header("Naming convention uCrystalUp, lAttackDown.")]
    [Header("first letter denotes ULDR direction.")]
    [Header("word at end denotes attack upward/downward attack")]
    public SpriteAnimator attackAnimator;
    public GroundMover groundMover;
    public SpriteRenderer attackRenderer;
    public string sortingLayerAboveCharacter;
    public string sortingLayerBelowCharacter;

    private bool areDiamondsUp = true;
    private bool isSlashAnimationDone = false;
    private PlayerActions playerActions;

    private void DiamondSlashFinished()
    {
        isSlashAnimationDone = true;
    }

    private void Start()
    {
        playerActions = PlayerActions.CreateWithDefaultBindings();
    }

    private void Update()
    {
        Vector2 facingDirection = groundMover.FaceDirection.ClosestCardinalDirection();
        facingDirection = (facingDirection == Vector2.zero) ? Vector2.down : facingDirection;

        if(facingDirection == Vector2.up)
        {
            attackRenderer.sortingLayerName = sortingLayerBelowCharacter;
        }
        else
        {
            attackRenderer.sortingLayerName = sortingLayerAboveCharacter;
        }


        if(playerActions.Attack.WasPressed)
        {
            PlayUpwardsAttack(facingDirection);
        }
        else if(playerActions.Attack.WasReleased)
        {
            PlayDownwardsAttack(facingDirection);
        }

        if (isSlashAnimationDone)
        {
            if(areDiamondsUp)
            {
                PlayDiamondsUp();
            }
            else
            {
                PlayDiamondsDown();
            }
        }
    }

    private void PlayUpwardsAttack(Vector2 attackDirection)
    {
        areDiamondsUp = false;
        isSlashAnimationDone = false;

        if (attackDirection == Vector2.up)
        {
            attackAnimator.Play("uAttackUp", false);
        }
        else if(attackDirection == Vector2.right)
        {
            attackAnimator.Play("rAttackUp", false);
        }
        else if(attackDirection == Vector2.down)
        {
            attackAnimator.Play("dAttackUp", false);
        }
        else if(attackDirection == Vector2.left)
        {
            attackAnimator.Play("lAttackUp", false);
        }
    }

    private void PlayDownwardsAttack(Vector2 attackDirection)
    {
        areDiamondsUp = true;
        isSlashAnimationDone = false;

        if (attackDirection == Vector2.up)
        {
            attackAnimator.Play("uAttackDown", false);
        }
        else if (attackDirection == Vector2.right)
        {
            attackAnimator.Play("rAttackDown", false);
        }
        else if (attackDirection == Vector2.down)
        {
            attackAnimator.Play("dAttackDown", false);
        }
        else if (attackDirection == Vector2.left)
        {
            attackAnimator.Play("lAttackDown", false);
        }
    }

    private void PlayDiamondsUp()
    {
        Vector2 facingDirection = groundMover.FaceDirection.ClosestCardinalDirection();
        if (facingDirection == Vector2.up)
        {
            attackAnimator.Play("uDiamondUp", true);
        }
        else if (facingDirection == Vector2.right)
        {
            attackAnimator.Play("rDiamondUp", true);
        }
        else if (facingDirection == Vector2.down)
        {
            attackAnimator.Play("dDiamondUp", true);
        }
        else if (facingDirection == Vector2.left)
        {
            attackAnimator.Play("lDiamondUp", true);
        }
    }

    private void PlayDiamondsDown()
    {
        Vector2 facingDirection = groundMover.FaceDirection.ClosestCardinalDirection();
        if (facingDirection == Vector2.up)
        {
            attackAnimator.Play("uDiamondDown", true);
        }
        else if (facingDirection == Vector2.right)
        {
            attackAnimator.Play("rDiamondDown", true);
        }
        else if (facingDirection == Vector2.down)
        {
            attackAnimator.Play("dDiamondDown", true);
        }
        else if (facingDirection == Vector2.left)
        {
            attackAnimator.Play("lDiamondDown", true);
        }
    }
}
