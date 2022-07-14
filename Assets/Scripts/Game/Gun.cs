using System.Collections;
using StarterAssets;
using Unity.Netcode;
using UnityEngine;

public class Gun : NetworkBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private float fireCooldown = 0.5f;
    
    
    private StarterAssetsInputs _input;
    private float _shootTimeoutDelta;
    
    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (!IsOwner) return;
        if (_input.shoot  && _shootTimeoutDelta <= 0.0f)
        {
            var position = transform.position;
            position.y = transform.position.y+ 1f;
            
            ShootServerRpc(position);
            _shootTimeoutDelta = fireCooldown;
            _input.shoot = false;
        }
        if (_shootTimeoutDelta >= 0.0f)
        {
            _shootTimeoutDelta -= Time.deltaTime;
        }
    }

    [ServerRpc]
    private void ShootServerRpc(Vector3 position, ServerRpcParams serverRpcParams = default)
    {
        var newProjectile = Instantiate(projectile, position, Quaternion.identity);
        var no=  newProjectile.GetComponent<NetworkObject>();
        no.SpawnWithOwnership(serverRpcParams.Receive.SenderClientId);
        
        newProjectile.MoveClientRpc(transform.forward);
        newProjectile.StartCoroutine(DestroyAfterTime(no));
    }
    
    
    private IEnumerator DestroyAfterTime(NetworkObject networkObject)
    {
        yield return new WaitForSeconds(3);
        networkObject.Despawn();
    }
}
