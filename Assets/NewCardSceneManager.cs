using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCardSceneManager : MonoBehaviour
{
    public NewCardPanel[] cardPanels;
    public Dice[] possibleDice;
    public Dice[] diceGrabBag;

    public Dice playerPick;
    public Dice[] defaultBag;
    void Awake()
    {
        cardPanels = transform.GetComponentsInChildren<NewCardPanel>();
        if(PlayerInfo.dieBag.Length == 0)
        {
            PlayerInfo.SetBag(defaultBag);
        }
    }
    void Start()
    {
        foreach(Dice dice in possibleDice)
        {
            int rarity = 10 - dice.rarity;
            Dice[] newBag = new Dice[diceGrabBag.Length + rarity];
            diceGrabBag.CopyTo(newBag, 0);
            for(int i = diceGrabBag.Length; i < newBag.Length; i++)
            {
                newBag[i] = dice;
            }
            diceGrabBag = newBag;
        }
        
        ShuffleSelection();
    }

    void LateUpdate()
    {

    }

    private IEnumerator WaitForShrink()
    {
        yield return new WaitForSeconds(NewCardPanel.SHRINKTIME);
        ShuffleSelection();
    }

    void ShuffleSelection()
    {
        playerPick = null;
        for (int i = 0; i < cardPanels.Length; i++)
        {   
            Dice endChoice = null;
            while(endChoice == null)
            {
                Dice selection = diceGrabBag[GetRandomDiceIndex()];
                bool isAlreadyChosen = false;
                foreach(NewCardPanel otherPanel in cardPanels)
                {
                    if(otherPanel.currentDice == selection) isAlreadyChosen = true;
                }
                if(!isAlreadyChosen) endChoice = selection;
            }
            cardPanels[i].ChangeDice(endChoice);
        }
    }
    private int GetRandomDiceIndex()
    {
        return Random.Range(0, diceGrabBag.Length);
    }
    public void PickNewDice(Dice newDice)
    {
        PlayerInfo.AddDiceToBag(newDice);
        foreach(NewCardPanel panel in cardPanels)
        {
            panel.ShrinkAway();
        }
        WaitForShrink();
    }
}
