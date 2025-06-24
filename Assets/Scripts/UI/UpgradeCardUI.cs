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
    [SerializeField] private Button upgradeButton;

    private WeaponStatsSO representedWeapon;

    public static event Action<WeaponStatsSO> OnUpgradeSelected;

    private void Awake()
    {
        upgradeButton.onClick.AddListener(HandleClick);
    }

    public void Setup(WeaponStatsSO weaponStats)
    {
        representedWeapon = weaponStats;
        icon.sprite = weaponStats.Icon;
        nameText.text = weaponStats.Name;
        descriptionText.text = weaponStats.Description;
    }

    private void HandleClick()
    {
        OnUpgradeSelected?.Invoke(representedWeapon);
    }

}
