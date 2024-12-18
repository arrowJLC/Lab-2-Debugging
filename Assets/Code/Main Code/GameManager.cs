using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;


[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public AudioMixerGroup SFXGroup;
    public AudioMixerGroup MusicGroup;

    [HideInInspector] public Action<int> OnLifeValueChanged;
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    //GAME PROPERTIES
    [SerializeField] private int maxLives = 1;
    private int _lives;
    public int lives
    {
        get => _lives;
        set
        {

            //do valid checking
            if (value < 0)
            {
                //GameOver();
                return;
            }

            if (_lives > value) Respawn();

            _lives = value;
            Debug.Log($"{_lives} lives left");
            OnLifeValueChanged?.Invoke(_lives);
        }
    }

    //GAME PROPERTIES

    //Player Controller information
    //[SerializeField] private PlayerController playerPrefab;
    //[HideInInspector] public PlayerController PlayerInstance => _playerInstance;
    //private PlayerController _playerInstance;

    private Transform currentCheckpoint;

    [HideInInspector]
    public MenuController currentMenuController;


    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            return;
        }

        Destroy(gameObject);
    }

    private void Start()
    {
        if (maxLives <= 0) maxLives = 1;
        lives = maxLives;
    }

    //void GameOver()
    //{
    //    Debug.Log("Game Over Should Go Here");
    //}

    void Respawn()
    {
        //we need some animation and then the respawn happens
       // _playerInstance.transform.position = currentCheckpoint.position;
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
       // _playerInstance = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
        currentCheckpoint = spawnLocation;
    }

    public void UpdateCheckpoint(Transform updatedCheckpoint)
    {
        currentCheckpoint = updatedCheckpoint;
        Debug.Log("Checkpoint updated");
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    currentMenuController.SetActiveState(MenuController.MenuStates.Pause);
        //}
    }
}