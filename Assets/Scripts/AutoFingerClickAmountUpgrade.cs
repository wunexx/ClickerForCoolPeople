using UnityEngine;

[CreateAssetMenu(fileName = "NewAutoFingerClickAmountUpgrade", menuName = "Upgrades/Auto Finger Click Amount Upgrade")]
public class AutoFingerClickAmountUpgrade : Upgrade
{
    [Header("Auto Finger Click Amount Upgrade")]
    [SerializeField] float additionalClickAmount;
    public override bool OnUpgrade(AudioSource audioSource)
    {
        if (!base.OnUpgrade(audioSource))
            return false;

        FingerManager fingerManager = FindFirstObjectByType<FingerManager>();
        fingerManager.UpgradeCurrentClickAmount(additionalClickAmount);

        return true;
    }
}
