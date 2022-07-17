using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public Sprite idleSprite;
    public Sprite hurtSprite;
    public Sprite attackSprite;
    public AttackPattern[] attackPool;
    public string[] monologuePool;
    public int health;
}
