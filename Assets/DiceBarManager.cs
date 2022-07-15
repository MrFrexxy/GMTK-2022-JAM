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


    public Dice[] testingBag;
    void Start()
    {
        PlayerInfo.dieBag = testingBag;
        RefillBag();
        diceSlots = transform.GetComponentsInChildren<DiceSlot>();
        foreach(DiceSlot slot in diceSlots)
        {
            int dicePick = Random.Range(0, currentBag.Count-1);
            slot.ChangeDice(currentBag[dicePick]);
            RemoveFromBag(dicePick);
        }
    }
    void LateUpdate()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {

        }
    }
    void RemoveFromBag(int index)
    {
        currentBag.RemoveAt(index);
        if(currentBag.Count == 0)
        {
            RefillBag();
        }
    }
    void RefillBag()
    {
        for(int i = 0; i < testingBag.Length; i++)
        {
            currentBag.Add(PlayerInfo.dieBag[i]);
        }
    }
    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform, 
            pointerData.position,
            canvas.worldCamera,
            out position);
    }
}
