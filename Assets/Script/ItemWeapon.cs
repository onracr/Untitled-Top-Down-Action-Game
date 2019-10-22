using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Weapon")]
public class ItemWeapon : Item
{
    public string weaponName;
    public int damage;
    public float pushForce;
}
