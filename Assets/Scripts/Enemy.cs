using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/Enemy")]
public class Enemy : ScriptableObject
{
    [Header("Basic")]
    public string Name;
    public Sprite Sprite;
    public float Health;

    [Header("Sounds")]
    public AudioClip[] DeathSounds;
    public AudioClip[] DamageSounds;
    public AudioClip BackgroundMusic;

    [Header("Rewards")]
    public float DeathReward;

    [Header("Effects")]
    public GameObject[] DamageEffects;
    public GameObject[] DeathEffects;
}
