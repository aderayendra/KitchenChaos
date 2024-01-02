using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;
    private Animator _animator;
    private static readonly int OpenCloseIndex = Animator.StringToHash(OpenClose);

    private const string OpenClose = "OpenClose";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerInteracted += ContainerCounterOnOnPlayerInteracted;
    }

    private void ContainerCounterOnOnPlayerInteracted(object sender, EventArgs e)
    {
        _animator.SetTrigger(OpenCloseIndex);
    }
}