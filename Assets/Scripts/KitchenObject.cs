using ScriptableObjects;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSo kitchenObjectSo;

    private IKitchenObjectParent _parent;

    public IKitchenObjectParent Parent
    {
        get => _parent;
        set
        {
            if (value.HasKitchenObject()) return;
            _parent?.ClearKitchenObject();
            _parent = value;
            var objectTransform = transform;
            objectTransform.parent = value.SpawnPoint;
            objectTransform.localPosition = Vector3.zero;
            value.KitchenObject = this;
        }
    }

    public KitchenObjectSo KitchenObjectSo => kitchenObjectSo;

    public void DestroySelf()
    {
        _parent = null;
        Destroy(gameObject);
    }

    public static KitchenObject Spawn(KitchenObjectSo kitchenObjectSo, IKitchenObjectParent parent)
    {
        GameObject newKitchenObject = Instantiate(kitchenObjectSo.Prefab);
        KitchenObject kitchenObject = newKitchenObject.GetComponent<KitchenObject>();
        kitchenObject.Parent = parent;
        return kitchenObject;
    }
}