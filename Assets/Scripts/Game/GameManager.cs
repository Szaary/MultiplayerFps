using UnityEngine;
using System.Collections;
using Unity.Netcode;

public class GameManager : SingletonNB<GameManager>
{
    [SerializeField] private float reaspawnTime = 3f;

    [SerializeField] private NetworkObject playerPrefab;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void Spawn(ulong ownerClientId)
    {
        Debug.Log("Starting respawn time");
        StartCoroutine(Respawn(ownerClientId));
    }

    private IEnumerator Respawn(ulong ownerClientId)
    {
        yield return new WaitForSeconds(reaspawnTime);

        Debug.Log("Respawning player");
        var player = Instantiate(playerPrefab);
        player.SpawnWithOwnership(ownerClientId);
    }
}
