using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Heal Self", menuName = "Dice Face Effects/Heal Self")]
public class HealSelf : DiceAction
{
    public override void DoAction(GameObject target, GameObject player, int value)
    {
        player.GetComponent<StatusManager>().AddHealth(value);
    }
}
