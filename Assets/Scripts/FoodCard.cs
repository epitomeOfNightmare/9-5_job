using UnityEngine;
using UnityEngine.UI;

public class FoodCard : MonoBehaviour
{
    public Image food;
    public Sprite foodCover, foodImage;
    public DeliveryManager deliveryManager;
    private string selectedFood;

    void Start()
    {
        
    }

    public void PassFood(Sprite image, string name)
    {
        foodImage = image;
        selectedFood = name;
    }
}
