using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dice", menuName = "Dice")]
public class Dice : ScriptableObject
{
    [Serializable]
    public class DiceFace
    {
    enum FaceType : int
    {
        Attack = 0,
        Defend = 1,
        Heal = 2,
        Special = 3
    }
    [SerializeField]
    private FaceType type;
    [SerializeField]
    private int value;
    [SerializeField]
    private DiceAction specialAction;
    public void ActivateFace(GameObject target)
    {
        if((int)type == 0)
        {
            target.GetComponent<StatusManager>().AddHealth(-value);
        }
        if((int)type == 1)
        {
            target.GetComponent<StatusManager>().AddBlock(value);
        }
        if((int)type == 2)
        {
            target.GetComponent<StatusManager>().AddHealth(value);
        }
        if((int)type == 3)
        {
            specialAction.DoAction(target);
        }
    }
    }
    public string diceName;
    public DiceFace[] faces;
    public int rarity;
}
