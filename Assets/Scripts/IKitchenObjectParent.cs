using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform SpawnPoint { get; }
    public KitchenObject KitchenObject { get; set; }
    public void ClearKitchenObject();
    public bool HasKitchenObject();
}