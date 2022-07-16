using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInfo
{    
    public const int ROLLCOUNT = 3;
    public static int stageNumber = 0;
    [SerializeField]
    public static Dice[] dieBag {get; set; }
    
    public static void AddDiceToBag(Dice newDice)
    {
        Dice[] newArray = new Dice[dieBag.Length + 1];
        dieBag.CopyTo(newArray, 0);
        newArray[dieBag.Length] = newDice;
        dieBag = newArray;
    }
    public static void SetBag(Dice[] newBag)
    {
        dieBag = newBag;
    }
}
