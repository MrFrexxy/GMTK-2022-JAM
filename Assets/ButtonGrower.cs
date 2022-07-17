using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonGrower : MonoBehaviour
{
    public RectTransform button;
    void Start()
    {
        button.GetComponent<Animator>().SetBool("HoveringBtn", false);
    }

    void OnPointerEnter(PointerEventData eventData)
    {
        button.GetComponent<Animator>().SetBool("HoveringBtn", true);
    }

    void OnPointerExit(PointerEventData eventData)
    {
        button.GetComponent<Animator>().SetBool("HoveringBtn", false);
    }
}
