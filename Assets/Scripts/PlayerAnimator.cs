using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;
    private const string IsWalkingKey = "IsWalking";
    private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash(IsWalkingKey);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(IsWalking, player.IsWalking());
    }
}