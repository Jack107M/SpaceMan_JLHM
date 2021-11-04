using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.menu;

    //Declarar variable estatica, sharedInstance se refiere a un singleton, una instancia compartida
    public static GameManager sharedInstance;

    private PlayerController controller;


    void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").
            GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && 
            currentGameState != GameState.inGame)
        {
            StartGame();
        }
    }

    //Aquí se realiza el funcionamiento para empezar el juego
    public void StartGame()
    {
        SetGameState(GameState.inGame);
    }

    //Aquí se realiza el funcionamiento para cuando se pierde una partida en el juego
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    //Aquí se realiza el funcionamiento para regresar al menú del juego
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    private void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {
            //TODO: colocar la lógica del menú
            MenuManager.sharedInstance.ShowMainMenu();

        } else if(newGameState == GameState.inGame)
        {
            //TODO: hay que preparar la escena para jugar

            LevelManager.sharedInstance.RemoveAllLevelBlocks();
            LevelManager.sharedInstance.GenerateInitialBlock();

            controller.StartGame();
            MenuManager.sharedInstance.HideMainMenu();

        }
        else if (newGameState == GameState.gameOver)
        {
            //TODO: preparar el juego para el Game Over
            MenuManager.sharedInstance.ShowMainMenu();
        }
        this.currentGameState = newGameState;
    }
}
