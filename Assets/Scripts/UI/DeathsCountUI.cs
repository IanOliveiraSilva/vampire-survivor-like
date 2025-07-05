using Survivor.Core.Events;
using TMPro;
using UnityEngine;

public class DeathsCountUI : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO onEnemyDeathEvent;
    [SerializeField] private TextMeshProUGUI deathCounterText;

    private int deathCount = 0;

    private void Start()
    {
        HandleEnemyDeath(0);
    }

    private void OnEnable()
    {
        onEnemyDeathEvent.OnEventRaised += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        onEnemyDeathEvent.OnEventRaised -= HandleEnemyDeath;
    }

    private void HandleEnemyDeath(int value)
    {
        deathCount += value;
        Debug.Log($"Enemy Death Count: {deathCount}");
        deathCounterText.text = $"{deathCount}";
    }
}
