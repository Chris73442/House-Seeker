using UnityEngine;
using TMPro;

public class UIHook : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    void Start()
    {
        GameManager.instance.SetScoreText(coinText);
    }
}