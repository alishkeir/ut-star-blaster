using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer = false;
    [SerializeField] int scoreValue = 50;

    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] bool applyCameraShake = false;

    CameraShake cameraShake;

    AudioManager audioManager;
    ScoreKeeper scoreKeeper;

    LevelManager levelManager;


    void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioManager = FindFirstObjectByType<AudioManager>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        levelManager = FindFirstObjectByType<LevelManager>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if (damageDealer)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitParticles();
            damageDealer.Hit();

            if (health <= 0)
            {
                audioManager.PlayDestroySFX();
            }
            else
            {
                audioManager.PlayDamageSFX();
            }

            if (applyCameraShake)
            {
                cameraShake.Play();
            }
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        if (isPlayer)
        {
            levelManager.LoadGameOver();
        }
        else
        {
            scoreKeeper.ModifyScore(scoreValue);
        }

        Destroy(gameObject);

    }

    void PlayHitParticles()
    {
        if (hitParticles)
        {
            ParticleSystem particles = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(particles, particles.main.duration + particles.main.startLifetime.constantMax);
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
