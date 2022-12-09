using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : Singleton<PlayerInput>
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform shootFrom;
    [SerializeField] float maxDistance;
    [SerializeField] float timeBetweenShots;
    [SerializeField] float reloadTime;
    [SerializeField] int shotStorage;

    PotionPanic inputActions;
    CannonPool _pool;

    bool shotHeld;

    float lastShotTime;
    float lastAddTime;

    public int shotsRemaining;

    bool canShoot => shotsRemaining > 0 && Time.time > lastShotTime + timeBetweenShots;
    bool shouldAdd => Time.time > lastAddTime + reloadTime;
    bool atCapacity => shotsRemaining >= shotStorage;

    private void OnEnable()
    {
        inputActions = new PotionPanic();
        inputActions.Player.Enable();
        inputActions.Player.Fire.performed += OnFire;
        inputActions.Player.Fire.canceled += OnFire;
    }

    private void OnDisable()
    {
        inputActions.Player.Fire.performed -= OnFire;
        inputActions.Player.Fire.canceled -= OnFire;
        inputActions.Player.Disable();
    }

    void Start()
    {
        _pool = gameObject.AddComponent<CannonPool>();
        _pool.ballPrefab = prefab;
    }

    private void Update()
    {
        HandleFire();

        if (GameManager.Instance.gameOver) return;
        if (atCapacity) return;
        if (!shouldAdd) return;

        shotsRemaining++;
        lastAddTime = Time.time;
    }


    private void OnFire(InputAction.CallbackContext context)
    {
        shotHeld = context.performed;
    }

    private void HandleFire()
    {
        if (!shotHeld) return;
        if (!canShoot) return;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            var targetPos = new Vector3(hit.point.x, 2.5f, -3f);
            ProjectileFire temp = _pool.TakeFromPool();
            temp.transform.position = shootFrom.position;
            temp.Launch(targetPos);
            lastShotTime = Time.time;

            if (shotsRemaining == shotStorage)
            {
                lastAddTime = Time.time;
            }

            shotsRemaining--;
        }
    }

}
