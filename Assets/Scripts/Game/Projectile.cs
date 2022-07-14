using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    
    [SerializeField] private float speed = 20;
    
    private bool canMove;
    private Vector3 _direction;
    
    
    [ClientRpc]
    public void MoveClientRpc(Vector3 direction)
    {
        _direction = direction;
        canMove = true;
    }

    private void Update()
    {
        if (canMove) transform.Translate(_direction * speed * Time.deltaTime);
    }
}