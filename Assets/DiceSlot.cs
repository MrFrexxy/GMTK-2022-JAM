using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiceSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{   
    private const int animationLength = 45;
    private const float ROLLING_ANIM_TIME = 0.2f;
    [SerializeField]
    private AnimationCurve mouseoverCurve;
    [SerializeField]
    private AnimationCurve fadeAwayCurve;
    public float animationFrame;
    private Dice currentDice;
    private Image spriteRenderer;
    private RectTransform rectTransform;
    [SerializeField]
    private RectTransform canvas;
    private Canvas thisCanvas;
    private bool isFocused;
    private bool isGrabbed;
    public bool inAttackPos;
    public bool isRolling;
    public bool isFading;
    private Vector2 originalPos;
    private Vector2 rollPos;
    private GameObject enemy;
    private DiceBarManager diceBarManager;
    private bool canRollDice;
    
    void Awake()
    {
        spriteRenderer = GetComponent<Image>();
        diceBarManager = GameObject.FindGameObjectWithTag("Dice Bar").GetComponent<DiceBarManager>();
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
        if(GameStateManager.currentState != GameStateManager.GameState.PlayerTurn) return;
        if(isFading) 
        {
            if(animationFrame > 0) 
            {
                animationFrame--;
                float sizeValue = mouseoverCurve.Evaluate(animationFrame/animationLength);
                rectTransform.localScale = Vector3.one * (sizeValue);
            }
            else if(animationFrame == 0)
            {
                RemoveDice();
                animationFrame--;
            }
            return;
        }

        if(isRolling) return;
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
        if(GameStateManager.currentState != GameStateManager.GameState.PlayerTurn) return;
        if(isGrabbed && Input.mousePosition.x > Screen.width/2)
        {
            inAttackPos = true;
        }
        else inAttackPos = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(diceBarManager.canRoll && !isRolling && isFocused && GameStateManager.currentState == GameStateManager.GameState.PlayerTurn)
        {
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
                StartCoroutine(RollDice());
            }
    }
    public void OnPointerMove(PointerEventData eventData)
    {
        if(isGrabbed) rectTransform.position = eventData.position;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isRolling) return;
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
        spriteRenderer.enabled = true;
    }
    public void RemoveDice()
    {
        currentDice = null;
        spriteRenderer.enabled = false;
        isFading = false;
        isRolling = false;
        isFocused = false;
        isGrabbed = false;
        rectTransform.position = originalPos;
        diceBarManager.canRoll = true;
        if(GameStateManager.turnsLeft == 0)
        {
            GameStateManager.ChangeState(GameStateManager.GameState.EnemyTurn);
            diceBarManager.RemoveAll();
        }
    }
    public IEnumerator RollDice()
    {
        GameStateManager.turnsLeft--;
        diceBarManager.canRoll = false;
        isRolling = true;
        int faceNum = 0;
        for(int i = 0; i < 6; i++)
        {
            faceNum = Random.Range(0, currentDice.faces.Length);
            spriteRenderer.sprite = currentDice.faces[faceNum].GetSprite();
            yield return new WaitForSeconds(ROLLING_ANIM_TIME);
        }
        currentDice.faces[faceNum].ActivateFace(enemy);
        yield return new WaitForSeconds(ROLLING_ANIM_TIME*3);
        isFading = true;
        animationFrame = animationLength;
    }
}
