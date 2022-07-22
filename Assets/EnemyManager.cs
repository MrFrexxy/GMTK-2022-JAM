using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    private const int MAX_LINES = 4;
    public TMP_Text textBox;
    public Image icon;
    public Enemy enemyData;
    public Enemy[] enemyOrder;
    private StatusManager statusManager;
    [SerializeField]
    private Sprite[] sprites;
    private Animator animator;
    public GameObject arena;
    
    private string[] text;
    void Awake()
    {
        icon = GetComponent<Image>();
        statusManager = GetComponent<StatusManager>();
        animator = GetComponent<Animator>();
        enemyData = enemyOrder[PlayerInfo.stageNumber];
        statusManager.SetMaxHealth(enemyData.health);
    }
    void Start()
    {
        text = new string[MAX_LINES];
        textBox.SetText("");
        sprites = new Sprite[3];
        sprites[0] = enemyData.idleSprite;
        sprites[1] = enemyData.hurtSprite;
        sprites[2] = enemyData.attackSprite;
        ChangeSprite(0);
        GameStateManager.ChangeState(GameStateManager.GameState.PlayerTurn);
    }
    public void StartTurn()
    {
        Debug.Log("StartTurn");
        //sends a random message from the enemy
        string lineToSend = enemyData.enemyName + ":" + enemyData.monologuePool[Random.Range(0, enemyData.monologuePool.Length)];
        textBox.SetText(lineToSend);
        //picks a random attack form attack pool and announces it*/
        AttackPattern nextAttack = enemyData.attackPool[Random.Range(0, enemyData.attackPool.Length)];
        //lineToSend = enemyData.enemyName + " used " + nextAttack.name;
        //SendLine(lineToSend, ref text);
        StartCoroutine(DoAttack(nextAttack));
    }

    public void EndTurn()
    {
        foreach (Transform child in arena.transform)
        {
        GameObject.Destroy(child.gameObject);
        }
        GameStateManager.ChangeState(GameStateManager.GameState.PlayerTurn);
    }

    private IEnumerator DoAttack(AttackPattern attackToDo)
    {
        
        float highestDelay = 0;
        for (int i = 0; i < attackToDo.projectiles.Length; i++)
        {
            if (attackToDo.projectiles[i].delay > highestDelay) highestDelay = attackToDo.projectiles[i].delay;
            Debug.Log(i);
            StartCoroutine(QueueProjectile(attackToDo.projectiles[i]));
        }
        animator.SetTrigger("isAttack");
        yield return new WaitForSeconds(highestDelay + attackToDo.endDelay);
        EndTurn();
    }
    private IEnumerator QueueProjectile(AttackPattern.ProjectileSpawn projectileSpawn)
    {
        yield return new WaitForSeconds(projectileSpawn.delay);
        GameObject proj = Instantiate(projectileSpawn.bulletType, projectileSpawn.spawnPos, Quaternion.identity);
        proj.transform.parent = gameObject.transform;
    }
    private void SendLine(string addedText, ref string[] fullText)
    {
        //if lines are all full shift up and add
        if(fullText[MAX_LINES-1] != null)
        {
            for(int i = 1; i < MAX_LINES; i++)
            {
                fullText[i-1] = fullText[i];
            }
            fullText[MAX_LINES-1] = addedText;
        }
        //otherwise set text to next open line
        else 
        for(int i = 0; i < MAX_LINES; i++)
        {
            if(fullText[i] == null)
            fullText[MAX_LINES-1] = addedText;
        }
        string newText = "";
        foreach(string t in fullText)
        {
            newText = newText + t + "\n";
        }
        textBox.SetText(newText);
    }
    public void ChangeSprite(int num)
    {
        icon.sprite = sprites[num];
    }
    public GameObject InstantiateToArena(GameObject newObject, Vector3 newPos)
    {
        GameObject obj = Instantiate(newObject, newPos, Quaternion.identity);
        obj.transform.parent = arena.transform;
        return obj;
    }
}
