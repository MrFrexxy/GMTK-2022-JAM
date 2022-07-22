using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shrink Player", menuName = "Dice Face Effects/Shrink Player")]
public class ShrinkPlayer : DiceAction
{
    public override void DoAction(GameObject target, GameObject player, int value)
    {
        player.GetComponent<MovementController>().ChangeSize(value);
    }
}
