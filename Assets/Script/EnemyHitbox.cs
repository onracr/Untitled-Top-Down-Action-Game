using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidables
{
    public int damage;
    public float pushForce;

    protected override void OnCollide(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            var dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = this.pushForce
            };
            GameManager.Instance.hitPoint -= damage;
            otherCollider.SendMessage("ReceiveDamage", dmg);
        }
    }
}
