using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Watches for attacks, raises an event if an attack connects
/// </summary>
public class Hurtable : MonoBehaviour
{
    [Tooltip("Attacks targeting this receiver type will connect")]
    public Attack.Recipient receiverType;
    [Tooltip("Attacks from the direction opposite this will connect")]
    public Attack.Direction openDirection;
    [Tooltip("Attacks from this orientation will connect")]
    public Attack.Orientation openOrientation;

    public Collider2D hurtableArea;
    public LayerMask attackMask;

    public HurtEvent OnHurt;

    private void Update()
    {
        Collider2D foundCollider = Physics2D.OverlapArea(hurtableArea.bounds.min, hurtableArea.bounds.max, attackMask);
        if(foundCollider != null)
        {
            //TODO: search for attacker and check if attack should hit you.
            //      if so, raise hurtevent.

        }
    }


    
}

[System.Serializable]
public class HurtEvent : UnityEvent<Attack>
{

}