using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "New Attack Pattern", menuName = "Attack")]
public class AttackPattern : ScriptableObject
{
    [Serializable]
    public class ProjectileSpawn
    {
        [SerializeField]
        private Vector2 spawnPos;
        [SerializeField]
        private float rotation;
        [SerializeField]
        private GameObject bulletType;
        [SerializeField]
        private float delay;
    }
    public ProjectileSpawn[] projectiles;
}
