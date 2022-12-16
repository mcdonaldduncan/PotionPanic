using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BasicSlime : MonoBehaviour, IEnemy, IPoolable<BasicSlime>
{
    [SerializeField] int startingHealth;
    [SerializeField] float speed;
    [SerializeField] Material medHealth;
    [SerializeField] Material lowHealth;
    [SerializeField] Material highHealth;

    Transform target;
    GameObject healthBar;
    MeshRenderer healthRend;

    Vector3 healthScale;

    bool isInitialized;

    public IObjectPool<BasicSlime> Pool { get; set; }

    public int Health { get; set; }

    public void CheckForDeath()
    {
        if (Health <= 0)
        {
            ReturnToPool();
        }
    }

    public void ScaleHealth()
    {
        float healthBarZ = (float)Health / (float)startingHealth;

        healthBar.transform.localScale = new Vector3(healthScale.x, healthScale.y, healthBarZ);

        if (Health <= (float)startingHealth / 3f)
        {
            healthRend.material = lowHealth;
        }
        else if (Health <= (float)startingHealth / 3f * 2f)
        {
            healthRend.material = medHealth;
        }
        else
        {
            healthRend.material = highHealth;
        }
    }

    public void ReturnToPool()
    {
        if (gameObject.activeSelf == true)
        {
            Pool.Release(this);
        }
    }

    public void TakeDamage(int damageTaken)
    {
        Health -= damageTaken;
        ScaleHealth();
        CheckForDeath();
    }

    private void OnEnable()
    {
        Health = startingHealth;
        if (!isInitialized)
        {
            Init();
        }
        ScaleHealth();
    }

    void Init()
    {
        isInitialized = true;
        healthBar = transform.GetChild(0).gameObject;
        healthRend = healthBar.GetComponent<MeshRenderer>();
        healthScale = healthBar.transform.localScale;
        target = GameObject.FindGameObjectWithTag("Target").transform;
    }

    void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if (transform.position == target.position)
        {
            ReturnToPool();

            if (GameManager.Instance.gameOver) return;

            GameManager.Instance.EndGame();
        }

    }

    public void Dispose()
    {
        ReturnToPool();
    }
}
