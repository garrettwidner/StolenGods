using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To be used in a set of 4 to trigger 4-directional attacks around player by calling AttackOccurred
/// </summary>
public class PlayerDirectionalAttacker : MonoBehaviour
{
    [SerializeField] private Attack.Direction direction;
    [SerializeField] private BoxCollider2D attackCollider;
    [SerializeField] private float attackDuration;

    private void Update()
    {
        //For TESTING
        if (attackCollider.enabled)
        {
            SuperDebugger.DrawX(attackCollider.bounds.center, attackCollider.bounds.extents.x, Color.red);
        }
    }

    public void AttackOccurred(Attack attack)
    {
        if (attack.direction == direction)
        {
            PlayAttack();
        }
    }

    private void PlayAttack()
    {
        attackCollider.enabled = true;
        Invoke("EndAttack", attackDuration);
    }

    private void EndAttack()
    {
        attackCollider.enabled = false;
    }

}

public class Attack
{
    public Orientation orientation;
    public Direction direction;
    public int power;

    public enum Orientation
    {
        up,
        down
    };

    public enum Direction
    {
        up,
        right,
        down,
        left
    };

    public Attack(Orientation o, Direction d)
    {
        orientation = o;
        direction = d;
        power = 1;
    }

    public Attack(Orientation o, Direction d, int pow)
    {
        orientation = o;
        direction = d;
        power = pow;
    }


}
