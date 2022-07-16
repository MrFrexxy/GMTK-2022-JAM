using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class NewCardPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    const float GROWTIME = 0.15f;
    const float GROWAMT = 0.1f;
    public const float SHRINKTIME = 1f;
    [SerializeField]
    private AnimationCurve growCurve;
    [SerializeField]
    private AnimationCurve shrinkCurve;
    [SerializeField]
    private NewCardSceneManager sceneManager;
    public Dice currentDice;
    [SerializeField]
    private Image imageComponent;
    private RectTransform imageRect;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text descText;
    private Coroutine currentAnim;
    public bool isVisible;
    public bool isSelected;
    void Awake()
    {
        currentDice = null;
        imageRect = imageComponent.gameObject.GetComponent<RectTransform>();
        sceneManager = GameObject.FindGameObjectWithTag("Main Canvas").GetComponent<NewCardSceneManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isSelected)
        {
            sceneManager.PickNewDice(currentDice);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeCoroutine(HoverOnAnim());
        isSelected = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeCoroutine(HoverOffAnim());
        isSelected = false;
    }
    public void ChangeDice(Dice newDice)
    {
        currentDice = newDice;
        imageComponent.sprite = currentDice.previewSprite;
        nameText.SetText(currentDice.name);
        descText.SetText(currentDice.description);
        ChangeCoroutine(GrowIn());
    }
    private IEnumerator HoverOnAnim()
    {
        float timeElapsed = 0;
        while(timeElapsed < GROWTIME)
        {
            float newScale = 1 + growCurve.Evaluate(timeElapsed/GROWTIME)*GROWAMT;
            imageRect.localScale = Vector3.one * newScale;
            Debug.Log(newScale);
            yield return null;
            timeElapsed += Time.deltaTime;
        }
    }
    private IEnumerator HoverOffAnim()
    {
        float timeElapsed = 0;
        while(timeElapsed < GROWTIME)
        {
            float newScale = 1 + (1 - growCurve.Evaluate(timeElapsed/GROWTIME))*GROWAMT;
            imageRect.localScale = Vector3.one * newScale;
            yield return null;
            timeElapsed += Time.deltaTime;
        }
    }
    
    private void ChangeCoroutine(IEnumerator routine)
    {
        if(currentAnim != null) StopCoroutine(currentAnim);
        currentAnim = StartCoroutine(routine);
    }
    public IEnumerator ShrinkAway()
    {
        float timeElapsed = 0;
        while(timeElapsed < GROWTIME)
        {
            float newScale = (1 - growCurve.Evaluate(timeElapsed/GROWTIME));
            imageRect.localScale = Vector3.one * newScale;
            yield return null;
            timeElapsed += Time.deltaTime;
        }
        isVisible = false;
    }
    private IEnumerator GrowIn()
    {
        float timeElapsed = 0;
        while(timeElapsed < GROWTIME)
        {
            float newScale = (growCurve.Evaluate(timeElapsed/GROWTIME));
            imageRect.localScale = Vector3.one * newScale;
            yield return null;
            timeElapsed += Time.deltaTime;
        }
        isVisible = true;
    }
    
}
