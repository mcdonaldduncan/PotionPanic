using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public delegate void GameEvent();
    public static event GameEvent GameStart;
    public static event GameEvent GameOver;

    public bool gameOver;

    private void Start()
    {
        StartGame();
    }

    public void EndGame()
    {
        gameOver = true;
        GameOver?.Invoke();
    }

    public void StartGame()
    {
        gameOver = false;
        GameStart?.Invoke();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
