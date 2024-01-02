using ScriptableObjects;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    // [SerializeField] private KitchenObjectSo kitchenObjectSo;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject() && !HasKitchenObject())
        {
            player.KitchenObject.Parent = this;
        }
        else if (!player.HasKitchenObject() && HasKitchenObject())
        {
            KitchenObject.Parent = player;
        }
    }
}