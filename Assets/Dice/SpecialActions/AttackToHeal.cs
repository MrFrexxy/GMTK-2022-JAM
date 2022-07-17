using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackToHeal : DiceAction
{
    public override void DoAction(GameObject target, GameObject player, int value)
    {
        //something like this
        /*
        GameObject[] attacks = FindObjectsOfType<EnemyAttack>();
        foreach(GameObject attack in attacks)
        {
            attack.ChangeType(Heal);
        }
        */
    }
}
