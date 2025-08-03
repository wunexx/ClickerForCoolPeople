using UnityEngine;

[CreateAssetMenu(fileName = "NewAutoFingerUpgrade", menuName = "Upgrades/Auto Finger Upgrade")]
public class AutoFingerUpgrade : Upgrade
{
    [Header("Auto Finger Upgrade")]
    [SerializeField] GameObject fingerPrefab;
    [SerializeField] float fingerSpawnRadius;

    public override bool OnUpgrade(AudioSource audioSource)
    {
        if (!base.OnUpgrade(audioSource))
            return false;

        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector2 finalPos = Vector2.zero + randomDir * fingerSpawnRadius;

        Vector2 rotDirection = (Vector2.zero - finalPos).normalized;
        float angle = Mathf.Atan2(rotDirection.y, rotDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        GameObject fingerObject = Instantiate(fingerPrefab, finalPos, targetRotation);

        Finger finger = fingerObject.GetComponent<Finger>();
        FingerManager fingerManager = FindFirstObjectByType<FingerManager>();

        finger.clickAmount = fingerManager.clickAmount;
        fingerManager.fingers.Add(finger);

        return true;
    }
}
