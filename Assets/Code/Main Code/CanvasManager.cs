using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Button")]
    
    public Button returnToGame;

    [Header("Menus")]
    public GameObject gameOver;
    [Header("Menus")]
    public GameObject level;

    // Start is called before the first frame update
    void Start()
    {
        //Button Bindings
        if (returnToGame) returnToGame.onClick.AddListener(() => SceneManager.LoadScene("Level"));


        //inital menu states
        if (gameOver)
            gameOver.SetActive(false);
        if (level)
            level.SetActive(true);






    }

}