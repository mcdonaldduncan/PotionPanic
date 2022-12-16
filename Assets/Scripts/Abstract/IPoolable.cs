using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public interface IPoolable<T> : IDisposable where T : Component
{
    public IObjectPool<T> Pool { get; set; }

    void ReturnToPool();
}
