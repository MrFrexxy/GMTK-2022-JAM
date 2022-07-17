using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPatternViewer : MonoBehaviour
{
    [SerializeField]
    AttackPattern pattern;
    [SerializeField]
    float iconSize;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for(int i = 0; i < pattern.projectiles.Length; i++)
        {
            Gizmos.DrawSphere(pattern.projectiles[i].spawnPos, iconSize);
        }
    }
}
