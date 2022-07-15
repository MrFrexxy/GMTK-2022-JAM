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
        public Vector2 spawnPos;
        [SerializeField]
        public float rotation;
        [SerializeField]
        public GameObject bulletType;
        [SerializeField]
        public float delay;
    }
    public ProjectileSpawn[] projectiles;


}
