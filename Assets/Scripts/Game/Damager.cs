using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Damager : NetworkBehaviour
{
    [SerializeField] private float damage = 10f;
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Damageable damageable))
        {
            if (damageable.OwnerClientId == OwnerClientId)
            {
                return;
            }
            damageable.TakeDamage(damage);
        }
    }
}
