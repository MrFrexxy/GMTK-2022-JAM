using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrowOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
public float animationLength;
public float animationFrame;
public AnimationCurve growCurve;
public float growAmt;
private RectTransform rectTransform;
private bool isFocused;
void Start()
{
rectTransform = GetComponent<RectTransform>();
}
void LateUpdate()
    {
    if(isFocused)
            {
                if(animationFrame < animationLength) animationFrame++;
                float sizeValue = growCurve.Evaluate(animationFrame/animationLength)*growAmt;
                rectTransform.localScale = Vector3.one * (sizeValue+1);
            }
            else
            {
                if(animationFrame > 0) animationFrame--;
                float sizeValue = growCurve.Evaluate(animationFrame/animationLength)*growAmt;
                rectTransform.localScale = Vector3.one * (sizeValue+1);
            }
    }
public void OnPointerEnter(PointerEventData eventData)
    {
        isFocused = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isFocused = false;
    }
}