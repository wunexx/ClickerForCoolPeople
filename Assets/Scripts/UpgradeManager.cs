using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    [Header("Other")]
    [SerializeField] Clicker clicker;
    [SerializeField] Upgrade[] allUpgrades;
    [SerializeField] Transform upgradeParent;
    [SerializeField] GameObject upgradeUIObjectPrefab;
    List<GameObject> upgradeUIObjects = new List<GameObject>();

    [Header("Sounds")]
    [SerializeField] AudioSource audioSource;

    private Upgrade[] runtimeUpgrades;

    private void Start()
    {
        runtimeUpgrades = new Upgrade[allUpgrades.Length];
        for (int i = 0; i < allUpgrades.Length; i++)
        {
            runtimeUpgrades[i] = Instantiate(allUpgrades[i]);
            CreateUpgradeUI(runtimeUpgrades[i]);
        }
    }
    void CreateUpgradeUI(Upgrade upgrade)
    {
        GameObject upgradeObject = Instantiate(upgradeUIObjectPrefab, Vector2.zero, Quaternion.identity, upgradeParent);
        upgradeUIObjects.Add(upgradeObject);

        TextMeshProUGUI upgradeName =  upgradeObject.GetComponentInChildren<TextMeshProUGUI>();
        upgradeName.text = upgrade.UpgradeName;

        Button upgradeButton = upgradeObject.GetComponentInChildren<Button>();
        upgradeButton.onClick.AddListener(() => upgrade.OnUpgrade(audioSource));

        TextMeshProUGUI upgradeCost = upgradeButton.GetComponentInChildren<TextMeshProUGUI>();
        upgradeCost.text = $"Cost: {upgrade.UpgradeCost}";
    }
    public void UpdateUpgradeUIs()
    {
        for (int i = 0; i < allUpgrades.Length; i++)
        {
            Button upgradeButton = upgradeUIObjects[i].GetComponentInChildren<Button>();

            TextMeshProUGUI upgradeCost = upgradeButton.GetComponentInChildren<TextMeshProUGUI>();
            upgradeCost.text = $"Cost: {runtimeUpgrades[i].UpgradeCost}";
        }
    }
}
