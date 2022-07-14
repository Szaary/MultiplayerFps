using System.Collections;
using StarterAssets;
using Unity.Netcode;
using UnityEngine;

public class Gun : NetworkBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private float fireCooldown = 0.2f;
    
    
    private StarterAssetsInputs _input;
    private float _shootTimeoutDelta;
    
    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (cameraController.cameraTarget != null)
        {
            Debug.DrawRay(cameraController.cameraTarget.transform.position, cameraController.cameraTarget.transform.forward, Color.green);
        }
#endif
        if (!IsOwner) return;
        if (_input.shoot  && _shootTimeoutDelta <= 0.0f)
        {
            var position = cameraController.cameraTarget.transform.position+cameraController.cameraTarget.transform.forward;
            var direction = cameraController.cameraTarget.transform.forward;
            
            ShootServerRpc(position, direction);
            _shootTimeoutDelta = fireCooldown;
            _input.shoot = false;
        }
        if (_shootTimeoutDelta >= 0.0f)
        {
            _shootTimeoutDelta -= Time.deltaTime;
        }
    }

    [ServerRpc]
    private void ShootServerRpc(Vector3 position, Vector3 direction, ServerRpcParams serverRpcParams = default)
    {
        var newProjectile = Instantiate(projectile, position, Quaternion.identity);
        
        newProjectile.NetworkObject.SpawnWithOwnership(serverRpcParams.Receive.SenderClientId);
        
        newProjectile.MoveClientRpc(direction);
        newProjectile.StartCoroutine(DestroyAfterTime(newProjectile.NetworkObject));
    }
    
    
    private IEnumerator DestroyAfterTime(NetworkObject networkObject)
    {
        yield return new WaitForSeconds(3);
        networkObject.Despawn();
    }
}
