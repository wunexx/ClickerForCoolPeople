using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] Clicker clicker;
    [Header("Sounds")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip upgradeSound;

    [Header("Upgrade 1")]
    [SerializeField] float upgrade1amount;
    [SerializeField] float upgrade1Cost;
    [SerializeField] TextMeshProUGUI upgrade1CostText;

    [Header("Upgrade 2")]
    [SerializeField] GameObject fingerPrefab;
    [SerializeField] float fingerUpgradeCost;
    [SerializeField] TextMeshProUGUI fingerUpgradeCostText;
    [SerializeField] float fingerSpawnRadius;

    private void Start()
    {
        upgrade1CostText.text = $"Cost: {upgrade1Cost}";
        fingerUpgradeCostText.text = $"Cost: {fingerUpgradeCost}";
    }
    public void Upgrade1()
    {
        if (clicker.CanSubtract(upgrade1Cost))
        {
            PlaySound();

            clicker.Subtract(upgrade1Cost);

            clicker.UpgradeClickAmount(upgrade1amount);
        }
    }
    public void Upgrade2()
    {
        if (clicker.CanSubtract(fingerUpgradeCost))
        {
            PlaySound();

            clicker.Subtract(fingerUpgradeCost);

            Vector2 randomDir = Random.insideUnitCircle.normalized;
            Vector2 finalPos = (Vector2)transform.position + randomDir * fingerSpawnRadius;

            Vector2 rotDirection = ((Vector2)transform.position - finalPos).normalized;

            float angle = Mathf.Atan2(rotDirection.y, rotDirection.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

            Instantiate(fingerPrefab, finalPos, targetRotation);
        }
    }
    void PlaySound()
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(upgradeSound);
    }
}
