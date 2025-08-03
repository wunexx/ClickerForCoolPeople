using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float maxHp = 100;
    [HideInInspector] public float currentHp;
    [SerializeField] protected float rewardGiven = 1000f;

    [SerializeField] protected AudioClip deathSoundEffect;
    protected AudioSource audioSource;
    [SerializeField] protected GameObject deathEffect;
    [SerializeField] protected float effectDestroyTime = 1.5f;
    
    protected virtual void Awake()
    {
        currentHp = maxHp;
        audioSource = GameObject.FindGameObjectWithTag("Main Button").GetComponent<AudioSource>();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0f)
        {
            OnDeath();
        }
    }
    protected virtual void OnDeath()
    {
        Clicker clicker = FindFirstObjectByType<Clicker>();
        clicker.Add(rewardGiven);

        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(deathSoundEffect);

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, effectDestroyTime);

        Destroy(gameObject);
    }
}
