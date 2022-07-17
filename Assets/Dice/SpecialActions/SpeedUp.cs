using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : DiceAction
{
    // Start is called before the first frame update
    public override void DoAction(GameObject target, GameObject player, int value)
    {
        player.GetComponent<MovementController>().moveSpeed += value;
    }
}
