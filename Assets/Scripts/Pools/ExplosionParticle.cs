using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ExplosionParticle : MonoBehaviour, IPoolable<ExplosionParticle>
{
    public IObjectPool<ExplosionParticle> Pool { get; set; }

    public void ReturnToPool()
    {
        if (gameObject.activeSelf == true)
        {
            Pool.Release(this);
        }
    }

    public void Dispose()
    {
        Pool.Release(this);
    }

    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), 1f);
    }

}
