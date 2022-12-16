using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class BasePool<T> : MonoBehaviour where T : Component, IPoolable<T>
{
    public int maxPoolSize = 50;
    public int stackDefaultCapacity = 30;
    public GameObject poolPrefab;

    public IObjectPool<T> Pool
    {
        get
        {
            if (_pool == null)
                _pool =
                    new ObjectPool<T>(
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

    private IObjectPool<T> _pool;

    private T CreatedPooledItem()
    {
        var go = Instantiate(poolPrefab);
        T poolItem = go.GetComponent<T>();
        if (poolItem != null)
        {
            poolItem.Pool = Pool;
            return poolItem;
        }
        else
        {
            return null;
        }
    }

    private void OnReturnedToPool(T poolItem)
    {
        poolItem.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(T poolItem)
    {
        poolItem.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(T poolItem)
    {
        Destroy(poolItem.gameObject);
    }

    internal T TakeFromPool()
    {
        T poolItem = Pool.Get();
        return poolItem;
    }

    public void SetPrefab(GameObject prefab)
    {
        poolPrefab = prefab;
    }
}
