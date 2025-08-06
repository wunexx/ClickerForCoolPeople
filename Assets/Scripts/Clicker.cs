using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    [Header("Click")]
    [SerializeField] float clickAmount = 1;

    [Header("Enemies")]
    [SerializeField] Enemy[] enemies;
    [SerializeField] int currentEnemyIndex = 0;
    Enemy currentEnemy;
    float currentEnemyHealth;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] TextMeshProUGUI enemyNameText;
    [SerializeField] Slider enemyHealthSlider;
    [SerializeField] TextMeshProUGUI enemyHealthText;

    [Header("Raycast")]
    [SerializeField] LayerMask buttonLayer;

    [Header("Animations")]
    [SerializeField] Animator animator;

    [Header("Audio")]
    [SerializeField] AudioSource clickAudioSource;
    [SerializeField] AudioSource bgMusicAudioSource;

    [Header("Sprites")]
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Collider")]
    [SerializeField] BoxCollider2D boxCollider2D;

    float currentClicks;
    private void Start()
    {
        SetNextEnemy();
    }
    public void UpdateUI()
    {
        amountText.text = currentClicks.ToString();

        enemyNameText.text = currentEnemy.Name;

        enemyHealthSlider.maxValue = currentEnemy.Health;
        enemyHealthSlider.value = currentEnemyHealth;

        enemyHealthText.text = currentEnemyHealth.ToString() + "/" + currentEnemy.Health.ToString();
    }

    void SetNextEnemy()
    {
        currentEnemy = enemies[currentEnemyIndex];
        currentEnemyHealth = currentEnemy.Health;
        UpdateUI();

        bgMusicAudioSource.clip = currentEnemy.BackgroundMusic;
        bgMusicAudioSource.Play();

        spriteRenderer.sprite = currentEnemy.Sprite;

        boxCollider2D.size = spriteRenderer.sprite.bounds.size;
        boxCollider2D.offset = spriteRenderer.sprite.bounds.center;

        currentEnemyIndex = (currentEnemyIndex + 1) % enemies.Length;
    }
    void OnEnemyDeath()
    {
        GameObject effect = Instantiate(currentEnemy.DeathEffects[Random.Range(0, currentEnemy.DeathEffects.Length)], transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);

        Add(currentEnemy.DeathReward);

        clickAudioSource.pitch = Random.Range(0.8f, 1.2f);
        clickAudioSource.volume = 0.6f;
        clickAudioSource.PlayOneShot(currentEnemy.DeathSounds[Random.Range(0, currentEnemy.DeathSounds.Length)]);

        SetNextEnemy();
    }

    public void Click(float amountGiven)
    {
        GameObject effect = Instantiate(currentEnemy.DamageEffects[Random.Range(0, currentEnemy.DamageEffects.Length)], transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);

        Add(amountGiven);
        currentEnemyHealth -= amountGiven;

        clickAudioSource.pitch = Random.Range(0.8f, 1.2f);
        clickAudioSource.volume = 0.35f;
        clickAudioSource.PlayOneShot(currentEnemy.DamageSounds[Random.Range(0, currentEnemy.DamageSounds.Length)]);

        animator.SetTrigger("Click");
    }
    private void Update()
    {
        if (currentEnemyHealth <= 0)
        {
            OnEnemyDeath();
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward, Mathf.Infinity, buttonLayer);
            if (!hit) return;

            Click(clickAmount);
        }
        UpdateUI();
    }
    public bool CanSubtract(float amount)
    {
        if ((currentClicks - amount) >= 0)
        {
            return true;
        }
        return false;
    }
    public void Add(float amount)
    {
        currentClicks += amount;
    }
    public void Subtract(float amount)
    {
        currentClicks -= amount;
    }
    public void UpgradeClickAmount(float amount)
    {
        clickAmount += amount;
    }
}