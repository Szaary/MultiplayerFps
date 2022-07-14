using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Damageable : NetworkBehaviour
{
    [SerializeField] private float health = 100;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Health: " + health);
        if (health <= 0)
        {
            Debug.Log("Dead");
            if (IsServer || IsHost)
            {
                DespawnPlayer();
            }
        }
    }

    private void DespawnPlayer()
    {
        GetComponent<NetworkObject>().Despawn();
    }
}