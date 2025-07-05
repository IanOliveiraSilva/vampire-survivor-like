using Survivor.Player.Data;
using Survivor.Weapons;
using Survivor.Weapons.Data;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Button upgradeButton;

    private WeaponStatsSO representedWeapon;
    private PlayerUpgradeSO representedStat;
    private bool isStatUpgrade;

    public static event Action<WeaponStatsSO> OnWeaponUpgradeSelected;
    public static event Action<PlayerUpgradeSO> OnPlayerStatUpgradeSelected;

    private void Awake()
    {
        upgradeButton.onClick.AddListener(HandleClick);
    }

    public void Setup(WeaponStatsSO weaponSO, RuntimeWeaponStats currentStats = null)
    {
        isStatUpgrade = false;
        representedWeapon = weaponSO;
        icon.sprite = weaponSO.icon;
        nameText.text = weaponSO.weaponName;

        if (currentStats == null)
        {
            descriptionText.text = weaponSO.description;
            levelText.text = "Novo!!";
        }
        else
        {
            int currentLevel = currentStats.CurrentLevel;
            int nextLevel = currentLevel;

            if (nextLevel >= weaponSO.upgradeLevels.Count)
            {
                descriptionText.text = "Nível máximo";
                levelText.text = $"{currentLevel} (MAX)";
                return;
            }

            var upgrade = weaponSO.upgradeLevels[nextLevel];
            descriptionText.text = string.IsNullOrWhiteSpace(upgrade.description)
                ? "Melhoria geral"
                : upgrade.description;

            levelText.text = $"{currentLevel} → {currentLevel + 1}";
        }
    }

    public void Setup(PlayerUpgradeSO statUpgrade, int currentLevel)
    {
        isStatUpgrade = true;
        representedStat = statUpgrade;
        icon.sprite = statUpgrade.icon;
        nameText.text = statUpgrade.statName;

        if (currentLevel >= 5)
        {
            descriptionText.text = "Nível máximo";
            levelText.text = $"{currentLevel} (MAX)";
        }
        else
        {
            descriptionText.text = statUpgrade.levelDescriptions[currentLevel];
            levelText.text = $"{currentLevel} → {currentLevel + 1}";
        }
    }

    private void HandleClick()
    {
        if (isStatUpgrade)
            OnPlayerStatUpgradeSelected?.Invoke(representedStat);
        else
            OnWeaponUpgradeSelected?.Invoke(representedWeapon);
    }
}
