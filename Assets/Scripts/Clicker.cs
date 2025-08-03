using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    [Header("Click")]
    [SerializeField] float givenPerClick = 1;

    [Header("Other")]
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] LayerMask buttonLayer;
    [SerializeField] GameObject effectPrefab;
    [SerializeField] Animator animator;
    [SerializeField] SecretBossSpawner secretBossSpawner;

    [Header("Audio")]
    [SerializeField] AudioSource clickAudioSource;
    [SerializeField] AudioClip clickSound;

    float count;
    public void UpdateUI()
    {
        amountText.text = count.ToString();
    }
    public void Click(float amountGiven)
    {
        GameObject effect = Instantiate(effectPrefab, Vector2.zero, Quaternion.identity);
        Destroy(effect, 1.5f);
        Add(amountGiven);
        clickAudioSource.pitch = Random.Range(0.8f, 1.2f);
        clickAudioSource.PlayOneShot(clickSound);
        animator.SetTrigger("Click");
    }
    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward, Mathf.Infinity, buttonLayer);
            if (!hit) return;

            if (hit.collider.CompareTag("Main Button"))
            {
                ClickAndDamage(givenPerClick);
            }
        }
        UpdateUI();
    }
    public void ClickAndDamage(float amount)
    {
        Click(amount);
        if (secretBossSpawner.currentBoss != null)
        {
            secretBossSpawner.currentBoss.GetComponent<Enemy>().TakeDamage(amount);
        }
    }
    public bool CanSubtract(float amount)
    {
        if ((count - amount) >= 0)
        {
            return true;
        }
        return false;
    }
    public void Add(float amount)
    {
        count += amount;
    }
    public void Subtract(float amount)
    {
        count -= amount;
    }
    public void UpgradeClickAmount(float amount)
    {
        givenPerClick += amount;
    }
}