using UnityEngine;

[CreateAssetMenu(fileName = "NewClickAmountUpgrade", menuName = "Upgrades/Click Amount Upgrade")]
public class ClickAmountUpgrade : Upgrade
{
    [Header("Click Amount Upgrade")]
    [SerializeField] float additionalClickAmount;
    public override bool OnUpgrade(AudioSource audioSource)
    {
        if (!base.OnUpgrade(audioSource))
            return false;

        GetClicker().UpgradeClickAmount(additionalClickAmount);

        return true;
    }
}
