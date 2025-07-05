using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action<int> OnCoinsChanged; // Evento para UI ou outros sistemas escutarem

    private int coinCount;

    public int CoinCount => coinCount;

    private void Awake()
    {
        // Singleton padrão
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0) return;

        coinCount += amount;
        OnCoinsChanged?.Invoke(coinCount);
    }

    public bool SpendCoins(int amount)
    {
        if (amount <= 0 || amount > coinCount) return false;

        coinCount -= amount;
        OnCoinsChanged?.Invoke(coinCount);
        return true;
    }
}
