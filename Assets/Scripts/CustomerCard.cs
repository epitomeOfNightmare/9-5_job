using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CustomerCard : MonoBehaviour
{
    public Image character;
    public Sprite characterCover, characterImage;
    public DeliveryManager deliveryManager;
    private string selectedCharacter;

    [SerializeField] private float rotateDuration = 1f;

    void Start()
    {
        StartCoroutine(PeekCard());
    }

    public void PassCharacter(Sprite image, string name)
    {
        characterImage = image;
        selectedCharacter = name;
    }

    public void ShowCard()
    {
        if (!deliveryManager.CanFlipCharacter()) return;
        StartCoroutine(TurnCard(0, 90, 180, characterImage));
    }
    public void HideCard()
    {
        StartCoroutine(TurnCard(180, 90, 0, characterCover));
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
        character.sprite = changeToSprite;
        timePassed = 0f;

        while (timePassed < rotateDuration)
        {
            timePassed += Time.deltaTime;
            float percentage = timePassed / rotateDuration;

            transform.localRotation = Quaternion.Slerp(halfwayPoint, endPoint, percentage);

            yield return null;
        }

        transform.localRotation = endPoint;
        if (changeToSprite == characterImage) deliveryManager.ReceiveCharacter(gameObject, selectedCharacter);
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
        character.sprite = characterImage;
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
        character.sprite = characterCover;
        timePassed = 0f;

        while ( timePassed < rotateDuration )
        {
            timePassed += Time.deltaTime;
            float percentage = timePassed / rotateDuration;

            transform.localRotation = Quaternion.Slerp(halfwayPoint, beginPoint, percentage);

            yield return null;
        }

        transform.localRotation = beginPoint;
    }
}
