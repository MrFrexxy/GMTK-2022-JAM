using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverScript : MonoBehaviour
{
    public void PlayGame()
    {
        AudioManager.instance.Stop("LOSE");
        SceneManager.LoadScene(0);
    }
}
