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
    public DiceSlot currentRolling;

    public Dice[] dieBag;
    public Dice[] defaultBag;
    void Start()
    {
        if(PlayerInfo.dieBag == null)
        {
            PlayerInfo.SetBag(defaultBag);
        }
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        dieBag = PlayerInfo.dieBag;
        RefillBag();
        diceSlots = transform.GetComponentsInChildren<DiceSlot>();
        ReDrawDice();
        foreach(Dice d in dieBag)
        {
            Debug.Log(d.diceName);
        }
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
        for(int i = 0; i < dieBag.Length; i++)
        {
            currentBag.Add(dieBag[i]);
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
