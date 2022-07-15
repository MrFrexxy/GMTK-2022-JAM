using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiceSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{   
    private const int animationLength = 45;
    [SerializeField]
    private AnimationCurve mouseoverCurve;
    private float animationFrame;
    private Dice currentDice;
    private Image spriteRenderer;
    private RectTransform rectTransform;
    [SerializeField]
    private RectTransform canvas;
    private Canvas thisCanvas;
    private bool isFocused;
    private bool isGrabbed;
    private Vector2 originalPos;
    
    void Awake()
    {
        spriteRenderer = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        thisCanvas = GetComponent<Canvas>();
    }
    void Start()
    {
        originalPos = rectTransform.position;
    }

    void LateUpdate()
    {
        if(isFocused)
        {
            if(animationFrame < animationLength) animationFrame++;
            float sizeValue = mouseoverCurve.Evaluate(animationFrame/animationLength);
            rectTransform.localScale = Vector3.one * (sizeValue+1);
        }
        else
        {
            if(animationFrame > 0) animationFrame--;
            float sizeValue = mouseoverCurve.Evaluate(animationFrame/animationLength);
            rectTransform.localScale = Vector3.one * (sizeValue+1);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(isFocused)
        {
            //Vector2 newPos;
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, eventData.position, null, out newPos);
            //rectTransform.position = eventData.position;
            isGrabbed = true;
            transform.SetAsLastSibling();
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
            isGrabbed = false;
            rectTransform.position = originalPos;
    }
    public void OnPointerMove(PointerEventData eventData)
    {
        if(isGrabbed) rectTransform.position = eventData.position;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isFocused = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isFocused = false;
    }

    public void ChangeDice(Dice newDice)
    {
        currentDice = newDice;
        spriteRenderer.sprite = currentDice.previewSprite;
    }
    public void RemoveDice()
    {
        currentDice = null;
        spriteRenderer.sprite = null;
    }
}
