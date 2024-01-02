using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform spawnPoint;

    public abstract void Interact(Player player);

    public virtual void InteractAlternate(Player player) => Debug.Log("not implemented");

    public Transform SpawnPoint => spawnPoint;

    private KitchenObject _kitchenObject;

    public KitchenObject KitchenObject
    {
        get => _kitchenObject;
        set => _kitchenObject = value;
    }

    public void ClearKitchenObject() => _kitchenObject = null;
    public bool HasKitchenObject() => _kitchenObject != null;
}