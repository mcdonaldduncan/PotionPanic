using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.GameOver += ShowUI;
        GameManager.GameStart += HideUI;
    }

    private void OnDisable()
    {
        GameManager.GameOver -= ShowUI;
        GameManager.GameStart -= HideUI;
    }

    public void ShowUI()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void HideUI()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
