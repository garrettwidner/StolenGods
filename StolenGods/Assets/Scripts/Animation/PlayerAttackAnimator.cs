using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public AttackEvent onAttackAnimated;

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
            if(onAttackAnimated!= null)
            {
                onAttackAnimated.Invoke(new Attack(Attack.Orientation.up, Attack.Direction.up));
            }
        }
        else if(attackDirection == Vector2.right)
        {
            attackAnimator.Play("rAttackUp", false);
            if (onAttackAnimated != null)
            {
                onAttackAnimated.Invoke(new Attack(Attack.Orientation.up, Attack.Direction.right));
            }
        }
        else if(attackDirection == Vector2.down)
        {
            attackAnimator.Play("dAttackUp", false);
            if (onAttackAnimated != null)
            {
                onAttackAnimated.Invoke(new Attack(Attack.Orientation.up, Attack.Direction.down));
            }
        }
        else if(attackDirection == Vector2.left)
        {
            attackAnimator.Play("lAttackUp", false);
            if (onAttackAnimated != null)
            {
                onAttackAnimated.Invoke(new Attack(Attack.Orientation.up, Attack.Direction.left));
            }
        }
    }

    private void PlayDownwardsAttack(Vector2 attackDirection)
    {
        areDiamondsUp = true;
        isSlashAnimationDone = false;

        if (attackDirection == Vector2.up)
        {
            attackAnimator.Play("uAttackDown", false);
            if (onAttackAnimated != null)
            {
                onAttackAnimated.Invoke(new Attack(Attack.Orientation.down, Attack.Direction.up));
            }
        }
        else if (attackDirection == Vector2.right)
        {
            attackAnimator.Play("rAttackDown", false);
            if (onAttackAnimated != null)
            {
                onAttackAnimated.Invoke(new Attack(Attack.Orientation.down, Attack.Direction.right));
            }
        }
        else if (attackDirection == Vector2.down)
        {
            attackAnimator.Play("dAttackDown", false);
            if (onAttackAnimated != null)
            {
                onAttackAnimated.Invoke(new Attack(Attack.Orientation.down, Attack.Direction.down));
            }
        }
        else if (attackDirection == Vector2.left)
        {
            attackAnimator.Play("lAttackDown", false);
            if (onAttackAnimated != null)
            {
                onAttackAnimated.Invoke(new Attack(Attack.Orientation.down, Attack.Direction.left));
            }
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

    [System.Serializable]
    public class AttackEvent : UnityEvent<Attack>
    {
    }
}
