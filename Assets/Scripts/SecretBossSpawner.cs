using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SecretBossSpawner : MonoBehaviour
{
    [Header("Boss Stats")]
    [SerializeField] float bossSpeed = 10f;

    [Header("Boss Spawn")]
    [Tooltip("can be 0 - 1, 0 = 0% | 1 = 100%")]
    [Range(0f, 1f)]
    [SerializeField] float bossSpawnChance = 0.5f;
    [SerializeField] float despawnDistance = 30f;

    [Tooltip("Cooldown for boss spawn \"roll\"")]
    [SerializeField] float bossSpawnCheckCooldown = 2f;

    [Header("Other")]
    [SerializeField] GameObject bossHpUIObject;
    [SerializeField] Slider bossHpSlider;
    [SerializeField] TextMeshProUGUI bossHpText;
    [SerializeField] GameObject bossPrefab;

    float bossSpawnCheckTimer;
    [HideInInspector] public GameObject currentBoss;

    private void Update()
    {
        if (currentBoss != null)
        {
            HandleBossUI();

            if (Vector2.Distance(currentBoss.transform.position, transform.position) >= despawnDistance)
            {
                Destroy(currentBoss);
            }

            return;
        }

        bossHpUIObject.SetActive(false);

        bossSpawnCheckTimer -= Time.deltaTime;

        if (bossSpawnCheckTimer <= 0)
        {
            if (Random.value < bossSpawnChance)
            {
                SpawnBoss();
            }
            bossSpawnCheckTimer = bossSpawnCheckCooldown;
        }
    }
    void HandleBossUI()
    {
        bossHpUIObject.SetActive(true);
        Enemy boss = currentBoss.GetComponent<Enemy>();
        bossHpSlider.maxValue = boss.maxHp;
        bossHpSlider.value = boss.currentHp;
        bossHpText.text = $"{boss.currentHp}/{boss.maxHp}";
    }
    public void SpawnBoss()
    {
        Vector2 spawnOffset = Random.Range(0, 2) == 0 ? new Vector2(-1, 0) : new Vector2(1, 0);

        Vector2 spawnPos = new Vector2(20, 0) * -spawnOffset;

        GameObject boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        currentBoss = boss;

        Rigidbody2D rb = boss.GetComponent<Rigidbody2D>();

        rb.linearVelocity = Vector2.right * spawnOffset * bossSpeed;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        //zabeyte
    }
#endif
}
