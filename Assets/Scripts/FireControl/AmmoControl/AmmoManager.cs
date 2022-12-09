using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [SerializeField] AmmoIndicator[] indicators;
    [SerializeField] Material[] materials;


    void Update()
    {
        for (int i = 0; i < indicators.Length; i++)
        {
            if (i >= PlayerInput.Instance.shotsRemaining)
            {
                indicators[i].SetMaterial(materials[1]);
            }
            else
            {
                indicators[i].SetMaterial(materials[0]);
            }
        }
    }
}
