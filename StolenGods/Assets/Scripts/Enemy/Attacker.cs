using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] protected Attack attack;

    /// <summary>
    /// To be called when an attack should occur
    /// </summary>
    /// <param name="attack"></param>
    public virtual void AttackOccurred()
    {
        PlayAttack();
    }

    protected virtual void PlayAttack()
    {
        attack.collider.enabled = true;
        Invoke("EndAttack", attack.duration);
    }

    protected virtual void EndAttack()
    {
        attack.collider.enabled = false;
    }

}

[System.Serializable]
public class Attack
{
    public Orientation orientation;
    public Direction direction;
    public Recipient recipient;
    public BoxCollider2D collider;
    public float duration;
    public int power;

    public enum Orientation
    {
        up,
        down,
        none
    };

    public enum Direction
    {
        up,
        right,
        down,
        left,
        all
    };

    public enum Recipient
    {
        player,
        enemy,
        both
    };

    public Attack(Orientation orientation, Direction direction, Recipient recipient, BoxCollider2D attackCollider, float attackDuration, int power)
    {
        this.orientation = orientation;
        this.direction = direction;
        this.recipient = recipient;
        this.collider = attackCollider;
        this.duration = attackDuration;
        this.power = power;
    }
}
