using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameStateManager
{
    public enum GameState : int
    {
        PlayerTurn = 0,
        EnemyTurn = 1,
        WinState = 2,
        LoseState = 3,
        Menu = 4
    }
    public static int turnsLeft;
    public static GameState currentState;
    public static void ChangeState(GameState newState)
    {
        if((newState != currentState) && (newState == GameState.PlayerTurn))
        {
            turnsLeft = 3;
            GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>().moveSpeed = PlayerInfo.PLAYERSPEED;
            GameObject.FindGameObjectWithTag("Player").transform.localScale = Vector3.one;
            GameObject.FindGameObjectWithTag("Dice Bar").GetComponent<DiceBarManager>().ReDrawDice();
        }
        if((newState != currentState) &&(newState == GameState.EnemyTurn))
        {
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyManager>().StartTurn();
        }
        if((newState != currentState) &&(newState == GameState.WinState))
        {
            if(PlayerInfo.stageNumber < 5)
            {
                PlayerInfo.stageNumber++;
                SceneManager.LoadScene(2);
            }
            else
            {
                SceneManager.LoadScene(3);
                AudioManager.instance.Play("WIN");
            }
            
        }
        if((newState != currentState) && (newState == GameState.LoseState))
        {
            //SceneManager.LoadScene(3);
        }
        currentState = newState;
    }
}
