public class CuttingCounter : BaseCounter
{
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

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            //cut the kitchen object
            KitchenObject.DestroySelf();
        }
    }
}