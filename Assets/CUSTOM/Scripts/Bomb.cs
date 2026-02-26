using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionRadius = 7f;
    public float explosionForce = 1000f;
    public float maxDamage = 50f;
    public GameObject particlePrefab;

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        GameObject p = Instantiate(particlePrefab, this.transform.position, Quaternion.identity);
        Destroy(p, 2f);

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
            EnemyHealth enemy = nearbyObject.GetComponentInParent<EnemyHealth>();
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