using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceBarManager : MonoBehaviour
{
    public DiceSlot[] diceSlots;
    public List<Dice> currentBag;
    [SerializeField]
    private Canvas canvas;
    public int rollsLeft;
    public GameObject enemy;
    public GameObject player;

    public Dice[] testingBag;
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerInfo.dieBag = testingBag;
        RefillBag();
        diceSlots = transform.GetComponentsInChildren<DiceSlot>();
        ReDrawDice();
    }
    public void RemoveFromBag(int index)
    {
        currentBag.RemoveAt(index);
        if(currentBag.Count == 0)
        {
            RefillBag();
        }
    }
    public void RefillBag()
    {
        for(int i = 0; i < testingBag.Length; i++)
        {
            currentBag.Add(testingBag[i]);
        }
    }

    public void RemoveAll()
    {
        foreach(DiceSlot slot in diceSlots)
        {
            if(slot.spriteRenderer.enabled == true) slot.RemoveDice();
        }
    }
    public void ReDrawDice()
    {
        foreach(DiceSlot slot in diceSlots)
        {
            int dicePick = Random.Range(0, currentBag.Count-1);
            slot.ChangeDice(currentBag[dicePick]);
            RemoveFromBag(dicePick);
        }
        rollsLeft = PlayerInfo.ROLLCOUNT;
    }
}
