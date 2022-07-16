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
    private string[] text;
    void Awake()
    {
        icon = GetComponent<Image>();
        statusManager = GetComponent<StatusManager>();
        enemyData = enemyOrder[PlayerInfo.stageNumber];
        statusManager.SetMaxHealth(enemyData.health);
    }
    void Start()
    {
        text = new string[MAX_LINES];
        textBox.SetText("");
        icon.sprite = enemyData.sprite;
        GameStateManager.ChangeState(GameStateManager.GameState.PlayerTurn);
    }
    public void StartTurn()
    {
        Debug.Log("StartTurn");
        //sends a random message from the enemy
        string lineToSend = enemyData.enemyName + ":" + enemyData.monologuePool[Random.Range(0, enemyData.monologuePool.Length)];
        SendLine(lineToSend, ref text);
        //picks a random attack form attack pool and announces it
        AttackPattern nextAttack = enemyData.attackPool[Random.Range(0, enemyData.attackPool.Length)];
        lineToSend = enemyData.enemyName + " used " + nextAttack.name;
        SendLine(lineToSend, ref text);
        StartCoroutine(DoAttack(nextAttack));
    }

    public void EndTurn()
    {
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
        yield return new WaitForSeconds(highestDelay + attackToDo.endDelay);
        EndTurn();
    }
    private IEnumerator QueueProjectile(AttackPattern.ProjectileSpawn projectileSpawn)
    {
        yield return new WaitForSeconds(projectileSpawn.delay);
        Instantiate(projectileSpawn.bulletType, projectileSpawn.spawnPos, Quaternion.identity);
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
}
