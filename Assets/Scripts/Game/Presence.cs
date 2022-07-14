using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Presence : NetworkBehaviour
{
    public override void OnNetworkDespawn()
    {
        base.OnNetworkSpawn();
        
        if(IsServer || IsHost)
        {
            Debug.Log("Preparing player to spawn");
            GameManager.Instance.Spawn(OwnerClientId);
        }
    }
    
}
