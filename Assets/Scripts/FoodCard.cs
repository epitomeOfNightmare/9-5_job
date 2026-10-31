using UnityEngine;
using UnityEngine.UI;

public class FoodCard : MonoBehaviour
{
    public Image food;
    public Sprite foodCover, foodImage;
    public DeliveryManager deliveryManager;
    private string selectedFood;
    [SerializeField] private string foodOptions;
    [SerializeField] private Sprite foodImageOptions;

    void Start()
    {
        
    }
}
