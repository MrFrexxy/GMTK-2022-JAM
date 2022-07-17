using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWall : DiceAction
{
    public GameObject wallObject;
    public override void DoAction(GameObject target, GameObject player, int value)
    {
        GameObject wallObj = Instantiate(wallObject, player.transform.position + (Vector3.up*value), Quaternion.identity);
        wallObj.transform.parent = target.transform;
    }
}
