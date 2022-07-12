using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Collectable
{
    [SerializeField] private Weapon _weapon;
    public Weapon WeaponData => _weapon;

    public new WeaponPickup Collect()
    {
        return this;
    } 
}
