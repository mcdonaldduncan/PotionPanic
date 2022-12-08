using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CannonPool : MonoBehaviour
{
    public int maxPoolSize = 50;
    public int stackDefaultCapacity = 30;
    public GameObject ballPrefab;

    public IObjectPool<ProjectileFire> Pool
    {
        get
        {
            if (_pool == null)
                _pool =
                    new ObjectPool<ProjectileFire>(
                        CreatedPooledItem,
                        OnTakeFromPool,
                        OnReturnedToPool,
                        OnDestroyPoolObject,
                        true,
                        stackDefaultCapacity,
                        maxPoolSize);
            return _pool;
        }
    }

    private IObjectPool<ProjectileFire> _pool;

    private ProjectileFire CreatedPooledItem()
    {
        var go = Instantiate(ballPrefab);
        ProjectileFire ball = go.GetComponent<ProjectileFire>();
        if (ball != null)
        {
            ball.Pool = Pool;
            return ball;
        }
        else
        {
            return null;
        }
    }

    private void OnReturnedToPool(ProjectileFire ball)
    {
        ball.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(ProjectileFire ball)
    {
        ball.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(ProjectileFire Ball)
    {
        Destroy(Ball.gameObject);
    }

    internal ProjectileFire TakeFromPool()
    {
        ProjectileFire currentBall = Pool.Get();
        return currentBall;
    }
}
