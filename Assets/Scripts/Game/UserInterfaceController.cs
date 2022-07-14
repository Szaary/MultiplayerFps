using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class UserInterfaceController : NetworkBehaviour
{
    [SerializeField] private UserInterface userInterfacePrefab;
    private UserInterface userInterface;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsOwner) return;
        
        if(userInterface == null)
        {
            userInterface = Instantiate(userInterfacePrefab);
            userInterface.Initialize(this);
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        Destroy(userInterface.gameObject);
    }
}
