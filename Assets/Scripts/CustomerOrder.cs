using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerOrder : MonoBehaviour
{
    public Image characterImage;
    public string characterName;
    public Image foodImage;
    public string foodName;

    public void AssignCharacter(Sprite image, string name)
    {
        characterImage.sprite = image;
        characterName = name;
    }
    public void AssignFood(Sprite image, string name)
    {
        foodImage.sprite = image;
        foodName = name;
    }

    public bool CheckRequest(string characterChoosen, string foodChoosen)
    {
        if (characterChoosen == characterName && foodChoosen == foodName) return true;
        else return false;
    }

    public string GetFoodName() { return foodName; }
    public Sprite GetFoodImage() {  return foodImage.sprite; }
    public string GetCharacterName() { return characterName; }
    public Sprite GetCharacterImage() { return characterImage.sprite; }
}
