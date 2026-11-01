using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class FoodCard : MonoBehaviour
{
    public Image food;
    public Sprite foodCover, foodImage;
    public DeliveryManager deliveryManager;
    private string selectedFood;

    [SerializeField] private float rotateDuration = 1f;

    void Start()
    {
        StartCoroutine(PeekCard());
    }

    public void PassFood(Sprite image, string name)
    {
        foodImage = image;
        selectedFood = name;
    }

    public void ShowCard()
    {
        if (!deliveryManager.CanFlipFood()) return;
        StartCoroutine(TurnCard(0, 90, 180, foodImage));
    }
    public void HideCard()
    {
        StartCoroutine(TurnCard(180, 90, 0, foodCover));
    }
    IEnumerator TurnCard(float startRotation, float halfRotation, float fullRotation, Sprite changeToSprite)
    {
        transform.localRotation = Quaternion.Euler(0f, startRotation, 0f);
        Quaternion beginPoint = transform.localRotation;
        Quaternion halfwayPoint = Quaternion.Euler(0f, halfRotation, 0f);
        Quaternion endPoint = Quaternion.Euler(0f, fullRotation, 0f);
        float timePassed = 0f;

        while (timePassed < rotateDuration)
        {
            timePassed += Time.deltaTime;
            float percentage = timePassed / rotateDuration;

            transform.localRotation = Quaternion.Slerp(beginPoint, halfwayPoint, percentage);

            yield return null;
        }

        transform.localRotation = halfwayPoint;
        food.sprite = changeToSprite;
        timePassed = 0f;

        while (timePassed < rotateDuration)
        {
            timePassed += Time.deltaTime;
            float percentage = timePassed / rotateDuration;

            transform.localRotation = Quaternion.Slerp(halfwayPoint, endPoint, percentage);

            yield return null;
        }

        transform.localRotation = endPoint;
        if (changeToSprite == foodImage) deliveryManager.ReceiveFood(gameObject, selectedFood);
    }

    private IEnumerator PeekCard()
    {
        Quaternion beginPoint = Quaternion.Euler(0f, 0f, 0f);
        Quaternion halfwayPoint = Quaternion.Euler(0f, 90f, 0f);
        Quaternion endPoint = Quaternion.Euler(0f, 180f, 0f);
        float timePassed = 0f;

        while (timePassed < rotateDuration)
        {
            timePassed += Time.deltaTime;
            float percentage = timePassed / rotateDuration;

            transform.localRotation = Quaternion.Slerp(beginPoint, halfwayPoint, percentage);

            yield return null;
        }

        transform.localRotation = halfwayPoint;
        food.sprite = foodImage;
        timePassed = 0f;

        while (timePassed < rotateDuration)
        {
            timePassed += Time.deltaTime;
            float percentage = timePassed / rotateDuration;

            transform.localRotation = Quaternion.Slerp(halfwayPoint, endPoint, percentage);

            yield return null;
        }

        transform.localRotation = endPoint;
        timePassed = 0f;
        yield return new WaitForSeconds(3f);

        while (timePassed < rotateDuration)
        {
            timePassed += Time.deltaTime;
            float percentage = timePassed / rotateDuration;

            transform.localRotation = Quaternion.Slerp(endPoint, halfwayPoint, percentage);

            yield return null;
        }

        transform.localRotation = halfwayPoint;
        food.sprite = foodCover;
        timePassed = 0f;

        while (timePassed < rotateDuration)
        {
            timePassed += Time.deltaTime;
            float percentage = timePassed / rotateDuration;

            transform.localRotation = Quaternion.Slerp(halfwayPoint, beginPoint, percentage);

            yield return null;
        }

        transform.localRotation = beginPoint;
    }
}
