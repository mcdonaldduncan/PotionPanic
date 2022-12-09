using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlime : MonoBehaviour, IEnemy
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

    public int Health { get; set; }

    public void CheckForDeath()
    {
        if (Health <= 0)
        {
            Destroy(this.gameObject);
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

    public void TakeDamage(int damageTaken)
    {
        Health -= damageTaken;
        ScaleHealth();
        CheckForDeath();
    }

    private void OnEnable()
    {
        Health = startingHealth;
    }

    void Start()
    {
        healthBar = transform.GetChild(0).gameObject;
        healthRend = healthBar.GetComponent<MeshRenderer>();
        healthScale = healthBar.transform.localScale;
        target = GameObject.FindGameObjectWithTag("Target").transform;
        ScaleHealth();
    }

    void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if (transform.position == target.position)
        {
            if (GameManager.Instance.gameOver) return;

            GameManager.Instance.EndGame();
        }

    }
}
