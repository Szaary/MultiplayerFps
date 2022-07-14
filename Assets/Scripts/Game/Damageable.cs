using System;
using Unity.Netcode;
using UnityEngine;

public class Damageable : NetworkBehaviour
{
    public event Action<float, float> OnHealthChanged;
    [SerializeField] private float maxHealth = 100;

    private float _currentHealth;

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Debug.Log("Health: " + _currentHealth);
        OnHealthChanged?.Invoke(damage, _currentHealth);
        if (_currentHealth <= 0)
        {
            Debug.Log("Dead");
            DespawnPlayer();
        }
    }

    private void DespawnPlayer()
    {
        Debug.Log("Despawning player");
        if (IsHost || IsServer)
        {
            Debug.Log("Despawning player object on server");
            NetworkObject.Despawn();
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        _currentHealth = maxHealth;
    }
}