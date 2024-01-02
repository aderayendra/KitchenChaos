using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu()]
    public class KitchenObjectSo : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Sprite sprite;
        [SerializeField] private new string name;

        public string Name => name;

        public Sprite Sprite => sprite;

        public GameObject Prefab => prefab;
    }
}