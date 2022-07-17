using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWall : DiceAction
{
    public GameObject[] wallObject;
    public override void DoAction(GameObject target, GameObject player, int value)
    {
        target.GetComponent<EnemyManager>().InstantiateToArena(wallObject[value], player.transform.position + Vector3.up * value/2);
    }
}
