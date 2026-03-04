using FMODUnity;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionRadius = 7f;
    public float explosionForce = 1000f;
    public float maxDamage = 50f;
    public GameObject SawBlade;

    [Header("Audio")]
    [SerializeField] private EventReference SawSound;

    private void Start()
    {
        RuntimeManager.PlayOneShotAttached(SawSound, SawBlade);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            // Apply explosion force if object has Rigidbody
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            // Apply damage if object has EnemyHealth script
            PlayerHealthh enemy = nearbyObject.GetComponentInParent<PlayerHealthh>();
            if (enemy != null)
            {
                float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);
                float damagePercent = 1 - (distance / explosionRadius);
                float finalDamage = maxDamage * Mathf.Clamp01(damagePercent);

                enemy.TakeDamage(finalDamage);
            }
        }

        Destroy(gameObject, 0.1f);
    }
}