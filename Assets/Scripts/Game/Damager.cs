using Unity.Netcode;
using UnityEngine;

public class Damager : NetworkBehaviour
{
    [SerializeField] private float damage = 10f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null)
        {
            return;
        }
        if (other.transform.parent.TryGetComponent(out Damageable damageable))
        {
            if (damageable.OwnerClientId == OwnerClientId)
            {
                return;
            }

            damageable.TakeDamage(damage, transform.position);
        }
    }
}