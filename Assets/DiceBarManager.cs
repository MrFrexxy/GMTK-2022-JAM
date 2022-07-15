using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBarManager : MonoBehaviour
{
    private DiceSlot[] diceSlots;
    private Dice[] currentBag;
    void Start()
    {
        currentBag = PlayerInfo.dieBag;

        diceSlots = transform.GetComponentsInChildren<DiceSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
