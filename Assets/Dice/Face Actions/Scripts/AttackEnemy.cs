using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Attack Enemy", menuName = "Dice Face Effects/Attack Enemy")]
public class AttackEnemy : DiceAction
{
    public override void DoAction(GameObject target, GameObject player, int value)
    {
        target.GetComponent<StatusManager>().AddHealth(-value);
    }
}
