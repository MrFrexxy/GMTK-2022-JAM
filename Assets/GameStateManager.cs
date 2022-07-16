using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateManager
{
    public enum GameState : int
    {
        PlayerTurn = 0,
        EnemyTurn = 1,
        Menu = 2
    }
    public static int turnsLeft;
    public static GameState currentState;
    public static void ChangeState(GameState newState)
    {
        currentState = newState;
        if(newState == GameState.PlayerTurn)
        {
            turnsLeft = 3;
        }
    }
}
