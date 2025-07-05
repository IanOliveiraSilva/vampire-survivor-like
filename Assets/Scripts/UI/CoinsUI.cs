using TMPro;
using UnityEngine;

public class CoinsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;

    private void Update()
    {
        coinsText.text = $"{GameManager.Instance.CoinCount}";
    }
}
