using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiceSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{   
    private const int animationLength = 45;
    private const float ROLLING_ANIM_TIME = 0.3f;
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
    private bool inAttackPos;
    private Vector2 originalPos;
    private GameObject enemy;
    
    void Awake()
    {
        spriteRenderer = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        thisCanvas = GetComponent<Canvas>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
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
        //Guard clause
        if(GameStateManager.currentState == GameStateManager.GameState.PlayerTurn) return;

        if(isGrabbed && Input.mousePosition.x > Screen.width/2)
        {
            inAttackPos = true;
        }
        else inAttackPos = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
        if(isFocused && GameStateManager.currentState == GameStateManager.GameState.PlayerTurn)
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
            if(!inAttackPos)
            rectTransform.position = originalPos;
            else
            {
                RollDice();
            }
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
    public IEnumerator RollDice()
    {
        int faceNum = 0;
        for(int i = 0; i < 6; i++)
        {
            faceNum = Random.Range(0, currentDice.faces.Length);
            spriteRenderer.sprite = currentDice.faces[faceNum].GetSprite();
            yield return new WaitForSeconds(ROLLING_ANIM_TIME);
        }
        currentDice.faces[faceNum].ActivateFace(enemy);
    }
}
