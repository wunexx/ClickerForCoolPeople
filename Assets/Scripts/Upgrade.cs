using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    [Header("Basic Settings")]
    public string UpgradeName;
    public float UpgradeCost = 50;
    [SerializeField] protected float upgradeCostIncreaseMultiplier = 1.2f;
    [SerializeField] protected AudioClip upgradeSound;

    public virtual bool OnUpgrade(AudioSource audioSource)
    {
        if (!GetClicker().CanSubtract(UpgradeCost))
            return false;

        PlaySound(audioSource);
        GetClicker().Subtract(UpgradeCost);

        UpgradeCost *= upgradeCostIncreaseMultiplier;
        UpgradeCost = Mathf.RoundToInt(UpgradeCost);

        GetUpgradeManager().UpdateUpgradeUIs();
        return true;
    }

    protected virtual Clicker GetClicker() => FindFirstObjectByType<Clicker>();
    protected virtual UpgradeManager GetUpgradeManager() => FindFirstObjectByType<UpgradeManager>();
    protected virtual void PlaySound(AudioSource audioSource)
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(upgradeSound);
    }
}

