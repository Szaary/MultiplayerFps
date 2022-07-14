using Cinemachine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : NetworkBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cameraPrefab;
    [SerializeField] private Transform cameraTarget;

    private CinemachineVirtualCamera _camera;
    
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsOwner) return;
        
        if (SceneManager.GetActiveScene().name == Scenes.PlayGround)
        {
            SetCameraForPlayer();
        }
        else
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene current, LoadSceneMode arg1)
    {
        if (current.name == Scenes.PlayGround)
        {
            SetCameraForPlayer();
        }
    }

    private void SetCameraForPlayer()
    {
        if (_camera == null)
        {
            var cameraFollower = Instantiate(cameraPrefab);
            cameraFollower.Follow = cameraTarget;
            _camera = cameraFollower;
        }
    }
}