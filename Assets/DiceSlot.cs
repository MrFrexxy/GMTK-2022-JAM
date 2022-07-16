using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiceSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{   
    private const int animationLength = 60;
    private const float ROLLING_ANIM_TIME = 0.1f;
    [SerializeField]
    private AnimationCurve mouseoverCurve;
    [SerializeField]
    private AnimationCurve fadeAwayCurve;
    public float animationFrame;
    private Dice currentDice;
    public Image spriteRenderer;
    private RectTransform rectTransform;
    [SerializeField]
    private RectTransform canvas;
    private Canvas thisCanvas;
    private bool isFocused;
    private bool isGrabbed;
    public bool inAttackPos;
    public bool isRolling;
    public bool isFading;
    private Vector3 originalPos;
    private Vector2 rollPos;
    private DiceBarManager diceBarManager;
    private bool canRollDice;
    
    void Awake()
    {
        spriteRenderer = GetComponent<Image>();
        diceBarManager = GameObject.FindGameObjectWithTag("Dice Bar").GetComponent<DiceBarManager>();
        rectTransform = GetComponent<RectTransform>();
        thisCanvas = GetComponent<Canvas>();
    }
    void Start()
    {
        originalPos = rectTransform.position;
    }

    void LateUpdate()
    {
        if(GameStateManager.currentState != GameStateManager.GameState.PlayerTurn) return;
        if(isRolling) return;
        if(isFading) 
        {
            if(animationFrame > 0) 
            {
                animationFrame--;
                float sizeValue = mouseoverCurve.Evaluate(animationFrame/(animationLength*2));
                rectTransform.localScale = Vector3.one * (sizeValue);
            }
            else if(animationFrame == 0)
            {
                RemoveDice();
                if(diceBarManager.rollsLeft == 0)
                {
                    GameStateManager.ChangeState(GameStateManager.GameState.EnemyTurn);
                    diceBarManager.RemoveAll();
                }
            }
            return;
        }
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
        if(isGrabbed && Input.mousePosition.x > Screen.width/2)
        {
            inAttackPos = true;
        }
        else inAttackPos = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(diceBarManager.rollsLeft != 0 && !isRolling && isFocused && GameStateManager.currentState == GameStateManager.GameState.PlayerTurn)
        {
            isGrabbed = true;
            transform.SetAsLastSibling();
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
            if(isGrabbed)
            { 
                isGrabbed = false;
                if(!inAttackPos)
                rectTransform.position = originalPos;
                else
                {
                    StartCoroutine(RollDice());
                }
            }
    }
    public void OnPointerMove(PointerEventData eventData)
    {
        if(isGrabbed) rectTransform.position = eventData.position;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!isRolling) isFocused = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isFocused = false;
    }

    public void ChangeDice(Dice newDice)
    {
        currentDice = newDice;
        spriteRenderer.sprite = currentDice.previewSprite;
        spriteRenderer.enabled = true;
    }
    public void RemoveDice()
    {
        currentDice = null;
        spriteRenderer.enabled = false;
        rectTransform.SetPositionAndRotation(originalPos, Quaternion.identity);
        isFading = false;
        isRolling = false;
        isFocused = false;
        isGrabbed = false;
    }
    public IEnumerator RollDice()
    {
        //decrements rollcount and changes to rollingState
        diceBarManager.rollsLeft--;
        isRolling = true;
        //then shuffles face a couple of times
        int faceNum = 0;
        for(int i = 0; i < 6; i++)
        {
            faceNum = Random.Range(0, currentDice.faces.Length);
            spriteRenderer.sprite = currentDice.faces[faceNum].GetSprite();
            yield return new WaitForSeconds(ROLLING_ANIM_TIME);
        }
        //activates last face rolled and fades away
        currentDice.faces[faceNum].ActivateFace(diceBarManager.enemy, diceBarManager.player);
        yield return new WaitForSeconds(ROLLING_ANIM_TIME*2);
        isRolling = false;
        isFading = true;
        animationFrame = animationLength*2;
    }
}
