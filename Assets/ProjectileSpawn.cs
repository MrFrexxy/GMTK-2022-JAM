using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ProjectileSpawn : ScriptableObject
{
    public Vector2 spawnPos;
    public GameObject bulletType;
    public float delay;
}
