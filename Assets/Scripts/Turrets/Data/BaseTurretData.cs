using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Turret/BaseTurretData", fileName = "BaseTurretData")]
public class BaseTurretData : ScriptableObject
{
    [SerializeField] private BaseTurret _turret = default;
    public BaseTurret Turret => _turret;
}
