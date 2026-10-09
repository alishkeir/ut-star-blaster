using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [SerializeField] private int damage = 10;
    [SerializeField] private int shotsToDestroy = 3;
    [SerializeField] ParticleSystem hitParticles;

    int ShotTaken = 0;

    float randomRotation = 0;

    private void Start()
    {
        randomRotation = Random.Range(0, 100);
    }

    private void Update()
    {
        transform.Rotate(randomRotation * Time.deltaTime * Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayHitParticles();


        if (other.gameObject.CompareTag("Player") && other.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage, false);
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.CompareTag("Player Bullet"))
        {
            ShotTaken++;

            AudioManager.Instance.PlayDamageSFX();

            if (ShotTaken >= shotsToDestroy)
            {
                AudioManager.Instance.PlayDestroySFX();
                Destroy(gameObject);
            }
            return;
        }

        AudioManager.Instance.PlayDestroySFX();
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
}
