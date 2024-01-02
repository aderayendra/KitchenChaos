using System;
using ScriptableObjects;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSo kitchenObjectSo;

    public event EventHandler OnPlayerInteracted;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()) return;
        KitchenObject.Spawn(kitchenObjectSo, player);
        OnPlayerInteracted?.Invoke(this, EventArgs.Empty);
    }
}