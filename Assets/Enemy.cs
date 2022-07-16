using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public Sprite sprite;
    public AttackPattern[] attackPool;
    public string[] monologuePool;
    public int health;
}
