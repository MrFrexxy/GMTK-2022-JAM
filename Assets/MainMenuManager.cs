using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public Dice[] defaultDiceBag;
    public void PlayGame()
    {
        PlayerInfo.dieBag = defaultDiceBag;
        SceneManager.LoadScene(1);
    }
}
