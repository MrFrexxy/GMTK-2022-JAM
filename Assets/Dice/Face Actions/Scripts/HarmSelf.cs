using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Harm Self", menuName = "Dice Face Effects/Harm Self")]
public class HarmSelf : DiceAction
{
    public override void DoAction(GameObject target, GameObject player, int value)
    {
        player.GetComponent<StatusManager>().AddHealth(-value);
    }
}
