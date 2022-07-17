using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPlayer : DiceAction
{
    public override void DoAction(GameObject target, GameObject player, int value)
    {
        player.GetComponent<MovementController>().ChangeSize(value);

    }
}
