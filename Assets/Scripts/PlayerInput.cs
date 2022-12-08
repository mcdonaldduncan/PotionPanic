using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform shootFrom;
    [SerializeField] float maxDistance;

    PotionPanic inputActions;

    CannonPool _pool;

    private void OnEnable()
    {
        inputActions = new PotionPanic();
        inputActions.Player.Enable();
        inputActions.Player.Fire.started += OnFire;
    }

    private void OnDisable()
    {
        inputActions.Player.Fire.started -= OnFire;
        inputActions.Player.Disable();
    }

    void Start()
    {
        _pool = gameObject.AddComponent<CannonPool>();
        _pool.ballPrefab = prefab;
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            //Instantiate(prefab, hit.point, Quaternion.identity);
            var targetPos = new Vector3(hit.point.x, 2.5f, -3f);
            ProjectileFire temp = _pool.TakeFromPool();
            temp.transform.position = shootFrom.position;
            temp.Launch(targetPos);
            
            
            
            
            //Instantiate(prefab, shootFrom.position, Quaternion.identity);
            //temp.GetComponent<ProjectileFire>().Launch(new Vector3(hit.point.x, 2.5f, -3f));
        }
    }

}
