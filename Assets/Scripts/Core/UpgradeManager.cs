using Survivor.Core.Events;
using Survivor.Player;
using Survivor.Player.Data;
using Survivor.Weapons;
using Survivor.Weapons.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private UpgradeCardUI[] upgradeCards;
    [SerializeField] private WeaponPoolSO weaponPool;
    [SerializeField] private PlayerUpgradeSO[] playerUpgrades;
    [SerializeField] private FloatEventChannelSO onLevelUpEvent;

    private WeaponController weaponController;
    private RuntimePlayerStats runtimeStats;

    private void Start()
    {
        weaponController = FindFirstObjectByType<WeaponController>();
        runtimeStats = FindFirstObjectByType<PlayerController>().GetRuntimePlayerStats();
    }

    private void OnEnable()
    {
        onLevelUpEvent.OnEventRaised += HandleLevelUp;
        UpgradeCardUI.OnWeaponUpgradeSelected += HandleWeaponUpgrade;
        UpgradeCardUI.OnPlayerStatUpgradeSelected += HandleStatUpgrade;
    }

    private void OnDisable()
    {
        onLevelUpEvent.OnEventRaised -= HandleLevelUp;
        UpgradeCardUI.OnWeaponUpgradeSelected -= HandleWeaponUpgrade;
        UpgradeCardUI.OnPlayerStatUpgradeSelected -= HandleStatUpgrade;
    }

    private void HandleLevelUp(float amount)
    {
        Time.timeScale = 0f;
        upgradePanel.SetActive(true);

        var weaponOptions = GetAvailableWeaponUpgrades();
        var statOptions = GetAvailableStatUpgrades();

        var mixed = weaponOptions.Cast<object>()
            .Concat(statOptions)
            .OrderBy(_ => Random.value)
            .Take(3)
            .ToList();

        for (int i = 0; i < upgradeCards.Length; i++)
        {
            if (i < mixed.Count)
            {
                upgradeCards[i].gameObject.SetActive(true);

                if (mixed[i] is WeaponStatsSO weapon)
                {
                    var currentStats = weaponController.GetActiveWeapons().FirstOrDefault(w => w.BaseStats == weapon);
                    upgradeCards[i].Setup(weapon, currentStats);
                }
                else if (mixed[i] is PlayerUpgradeSO stat)
                {
                    int currentLevel = runtimeStats.GetStatLevel(stat.statType);
                    upgradeCards[i].Setup(stat, currentLevel);
                }
            }
            else
            {
                upgradeCards[i].gameObject.SetActive(false);
            }
        }
    }

    private List<WeaponStatsSO> GetAvailableWeaponUpgrades()
    {
        var list = new List<WeaponStatsSO>();
        var allWeapons = weaponPool.availableWeapons;
        var activeStats = weaponController.GetActiveWeapons();

        foreach (var weapon in allWeapons)
        {
            var runtime = activeStats.FirstOrDefault(w => w.BaseStats == weapon);
            if (runtime == null || runtime.CurrentLevel < weapon.upgradeLevels.Count)
            {
                list.Add(weapon);
            }
        }

        return list;
    }

    private List<PlayerUpgradeSO> GetAvailableStatUpgrades()
    {
        return playerUpgrades
            .Where(upg => runtimeStats.GetStatLevel(upg.statType) < 5)
            .ToList();
    }

    private void HandleWeaponUpgrade(WeaponStatsSO selected)
    {
        weaponController.AddOrUpgradeWeapon(selected);
        ClosePanel();
    }

    private void HandleStatUpgrade(PlayerUpgradeSO selected)
    {
        runtimeStats.UpgradeStat(selected.statType);
        ClosePanel();
    }

    private void ClosePanel()
    {
        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
