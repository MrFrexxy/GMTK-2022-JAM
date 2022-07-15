using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "New Attack Pattern", menuName = "Attack")]
public class AttackPattern : ScriptableObject
{
    public ProjectileSpawn[] projectiles;
}
