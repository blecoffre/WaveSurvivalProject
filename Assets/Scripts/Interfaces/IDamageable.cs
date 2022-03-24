﻿using System.Collections;
using UnityEngine;

public interface IDamageable<T>
{
    public void TakeDamage(T damageTaken);
}