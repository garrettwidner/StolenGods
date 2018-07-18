using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To be used in a set of 4 to trigger 4-directional attacks around player by calling AttackOccurred
/// </summary>
public class PlayerDirectionalAttacker : Attacker
{
    private void Update()
    {
        //For TESTING
        if (attack.collider.enabled)
        {
            SuperDebugger.DrawX(attack.collider.bounds.center, attack.collider.bounds.extents.x, Color.red);
        }
    }

    public void AttackOccurred(Attack.Direction direction, Attack.Orientation orientation)
    {
        if(direction == attack.direction && orientation == attack.orientation)
        {
            PlayAttack();
        }
    }
}


