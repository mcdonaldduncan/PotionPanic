using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    int Health { get; set; }

    void TakeDamage(int damageTaken);

    void CheckForDeath();

    void ScaleHealth();
}
