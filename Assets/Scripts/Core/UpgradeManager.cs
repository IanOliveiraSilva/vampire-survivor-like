using Survivor.Core.Events;
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

    [SerializeField] private Survivor.Core.Events.FloatEventChannelSO onLevelUpEvent;

    private Survivor.Weapons.WeaponController weaponController;

    private void Awake()
    {
        weaponController = FindFirstObjectByType<Survivor.Weapons.WeaponController>();
    }

    private void OnEnable()
    {
        onLevelUpEvent.OnEventRaised += HandleLevelUp;
        UpgradeCardUI.OnUpgradeSelected += HandleUpgradeSelected;
    }

    private void OnDisable()
    {
        onLevelUpEvent.OnEventRaised -= HandleLevelUp;
        UpgradeCardUI.OnUpgradeSelected -= HandleUpgradeSelected;
    }

    private void HandleLevelUp(float amount)
    {
        Time.timeScale = 0f;

        List<WeaponStatsSO> options = GetRandomUpgrades(3);

        upgradePanel.SetActive(true);

        for (int i = 0; i < upgradeCards.Length; i++)
        {
            if (i < options.Count)
            {
                upgradeCards[i].gameObject.SetActive(true);
                upgradeCards[i].Setup(options[i]);
            }
            else
            {
                upgradeCards[i].gameObject.SetActive(false);
            }
        }
    }

    private void HandleUpgradeSelected(WeaponStatsSO selectedWeapon)
    {
        weaponController.AddWeapon(selectedWeapon);

        upgradePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    private List<WeaponStatsSO> GetRandomUpgrades(int count)
    {
        List<WeaponStatsSO> available = new List<WeaponStatsSO>(weaponPool.availableWeapons);
        List<WeaponStatsSO> currentWeapons = weaponController.GetActiveWeaponStats();

        available.RemoveAll(weapon => currentWeapons.Contains(weapon));

        return available.OrderBy(x => Random.value).Take(count).ToList();
    }
}