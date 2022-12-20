using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;



    private void OnEnable()
    {
        GameManager.GameOver += ShowUI;
        GameManager.GameStart += HideUI;
        GameManager.GameOver += SetText;
    }

    private void OnDisable()
    {
        GameManager.GameOver -= ShowUI;
        GameManager.GameStart -= HideUI;
        GameManager.GameOver -= SetText;
    }

    public void ShowUI()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void HideUI()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SetText()
    {
        timeText.text = $"You lasted {Time.time} seconds!";
    }
}
