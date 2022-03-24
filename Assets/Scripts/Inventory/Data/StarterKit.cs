using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Inventory/StarterKit", fileName = "PlayerStarterKit")]
public class StarterKit : ScriptableObject
{
    [SerializeField] private Weapon[] _startingWeapons = default;

    public Weapon[] GetStartingWeapons()
    {
        return _startingWeapons;
    }
}
