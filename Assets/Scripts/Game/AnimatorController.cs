using StarterAssets;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class AnimatorController : NetworkBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NetworkAnimator networkAnimator;


    private StarterAssetsInputs _input;
    private static readonly int IsFiring = Animator.StringToHash("isFiring");
    private static readonly int Speed = Animator.StringToHash("speed");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    private Vector3 previous;
    private float velocity;
    private void Update()
    {
        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
        animator.SetFloat(Speed, velocity);
        
        
        if (!IsOwner) return;

        if (_input.shoot)
        {
            networkAnimator.SetTrigger(IsFiring);
        }

        if (_input.jump)
        {
            networkAnimator.SetTrigger(IsJumping);
            JumpServerRpc();
        }
    }

    [ServerRpc]
    private void JumpServerRpc()
    {
        JumpClientRpc();
    }

    [ClientRpc]
    private void JumpClientRpc()
    {
        if (IsOwner) return;
        networkAnimator.SetTrigger(IsJumping);
    }
}