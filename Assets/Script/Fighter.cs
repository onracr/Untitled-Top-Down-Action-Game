using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    // Public fields
    public int hitPoint = 10;
    public int maxHitPoint = 10;
    public float pushRecoverySpeed = .2f;
    
    // Immunity
    protected float ImmuneTime = .5f;
    protected float LastImmune;
    
    // Push
    protected Vector3 PushDirection;

    protected virtual void Start()
    {
        GameManager.Instance.hitPoint = maxHitPoint;
    }

    // All fighters can receive damage and die
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - LastImmune > ImmuneTime)
        {
            LastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            PushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.Instance.ShowText(
                dmg.damageAmount.ToString(), 21, Color.red,
                transform.position, Vector3.up * 20, .5f);

            if (hitPoint <= 0)
            {
                hitPoint = 0;
                Die();
            }
        }
    }

    protected virtual void Die()
    {
        
    }
}
