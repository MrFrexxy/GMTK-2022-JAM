using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Wall", menuName = "Dice Face Effects/Create Wall")]
public class CreateWall : DiceAction
{
    public GameObject wallObject;
    public override void DoAction(GameObject target, GameObject player, int value)
    {
        GameObject newWall = target.GetComponent<EnemyManager>().InstantiateToArena(wallObject, player.transform.position + (Vector3.up * value / 4));
        newWall.transform.localScale = Vector3.one*0.25f*value;
    }
}
