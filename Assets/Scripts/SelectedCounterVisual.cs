using UnityEngine;
using UnityEngine.Serialization;

public class SelectedCounterVisual : MonoBehaviour
{
    [FormerlySerializedAs("clearCounter")] [SerializeField]
    private BaseCounter kitchenCounter;

    [FormerlySerializedAs("visualGameObject")] [SerializeField]
    private GameObject[] visualGameObjects;

    private void Start()
    {
        Player.Instance.OnSelectedKitchenCounterChanged += Player_OnSelectedKitchenCounterChanged;
    }

    private void Player_OnSelectedKitchenCounterChanged(object sender, Player.OnSelectedKitchenCounterChangedEventArgs e)
    {
        foreach (GameObject visualGameObject in visualGameObjects)
        {
            if (e.KitchenCounter == kitchenCounter && !visualGameObject.activeSelf)
            {
                visualGameObject.SetActive(true);
            }
            else if (e.KitchenCounter != kitchenCounter && visualGameObject.activeSelf)
            {
                visualGameObject.SetActive(false);
            }
        }
    }
}