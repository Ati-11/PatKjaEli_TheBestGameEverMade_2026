using UnityEngine;
using Alteruna;

public class PlayerHealthh : AttributesSync
{
    public float maxHealth = 100f;
    private float currentHealth;

    private Alteruna.Avatar avatar;

    void Start()
    {
        avatar = GetComponent<Alteruna.Avatar>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (!avatar.IsMe) return; // Only owner changes health

        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            BroadcastRemoteMethod("Die");
        }
    }

    [SynchronizableMethod]
    void Die()
    {
        Debug.Log("player died");
    }
}