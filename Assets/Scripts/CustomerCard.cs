using UnityEngine;
using UnityEngine.UI;

public class CustomerCard : MonoBehaviour
{
    public Image character;
    public Sprite characterCover, characterImage;
    public DeliveryManager deliveryManager;
    private string selectedCharacter;

    void Start()
    {

    }

    public void PassCharacter(Sprite image, string name)
    {
        characterImage = image;
        selectedCharacter = name;
    }
}
