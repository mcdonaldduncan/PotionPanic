using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject particlePrefab;

    public delegate void GameEvent();
    public static event GameEvent GameStart;
    public static event GameEvent GameOver;

    [System.NonSerialized] public bool gameOver;

    [System.NonSerialized] public ParticlePool ParticlePool;

    private void Start()
    {
        StartGame();

        ParticlePool = gameObject.AddComponent<ParticlePool>();
        ParticlePool.poolPrefab = particlePrefab;
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
