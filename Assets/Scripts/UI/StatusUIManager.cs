using Survivor.Player;
using Survivor.Player.Data;
using Survivor.Weapons;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // 1. Adicionar o namespace do Input System

public class StatusUIManager : MonoBehaviour
{
    [Header("Configuração da UI")]
    [SerializeField] private GameObject statusPanel;
    [SerializeField] private Transform itemsContainer;
    [SerializeField] private GameObject statusItemPrefab;

    [Header("Dados do Jogo")]
    [SerializeField] private PlayerUpgradeSO[] allPlayerUpgrades;

    private PlayerController playerController;
    private WeaponController weaponController;
    private RuntimePlayerStats runtimeStats;

    private bool isPanelOpen = false;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        weaponController = FindFirstObjectByType<WeaponController>();
        runtimeStats = playerController.GetRuntimePlayerStats();

        statusPanel.SetActive(false);
    }

    // 2. O método Update() foi removido. Não precisamos mais dele!

    /// <summary>
    /// Esta é a nova função que será chamada pelo PlayerInput Component.
    /// O nome pode ser qualquer um, mas deve ser público.
    /// </summary>
    public void OnPause(InputAction.CallbackContext context)
    {
        // Queremos executar a ação apenas quando o botão for pressionado (performed)
        // Isso evita que a ação seja chamada múltiplas vezes (ex: no momento de soltar o botão)
        if (context.performed)
        {
            TogglePanel();
        }
    }

    private void TogglePanel()
    {
        isPanelOpen = !isPanelOpen;
        statusPanel.SetActive(isPanelOpen);

        if (isPanelOpen)
        {
            Time.timeScale = 0f;
            UpdateStatusDisplay();
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void UpdateStatusDisplay()
    {
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }

        var activeWeapons = weaponController.GetActiveWeapons();
        foreach (var weapon in activeWeapons)
        {
            GameObject itemGO = Instantiate(statusItemPrefab, itemsContainer);
            StatusItemUI itemUI = itemGO.GetComponent<StatusItemUI>();

            int maxLevel = weapon.BaseStats.upgradeLevels.Count;
            itemUI.Setup(weapon.BaseStats.icon, weapon.BaseStats.weaponName, weapon.CurrentLevel, maxLevel);
        }

        foreach (var statUpgrade in allPlayerUpgrades)
        {
            int currentLevel = runtimeStats.GetStatLevel(statUpgrade.statType);
            if (currentLevel > 0)
            {
                GameObject itemGO = Instantiate(statusItemPrefab, itemsContainer);
                StatusItemUI itemUI = itemGO.GetComponent<StatusItemUI>();

                int maxLevel = 5;
                itemUI.Setup(statUpgrade.icon, statUpgrade.name, currentLevel, maxLevel);
            }
        }
    }
}