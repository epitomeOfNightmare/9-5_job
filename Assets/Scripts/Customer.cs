using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public string[] foods;
    public Sprite[] foodImages;
    public GameObject ordering;
    public Image orderImage;
    private Image customer;
    private Color customerTransparency;
    private string customerOrder;

    private void Awake()
    {
        customer = GetComponent<Image>();
        customerTransparency = customer.color;
    }

    private void OnEnable()
    {
        CustomerState(true);
    }

    public void CustomerState(bool state)
    {
        if (state) StartCoroutine(FadeCustomer(0, 1, state));
        else StartCoroutine(FadeCustomer(1, 0, state));
    }
    public IEnumerator FadeCustomer(float startAlpha, float endAlpha, bool state)
    {
        float timeElasped = 0f;

        while (timeElasped < 1.5f)
        {
            timeElasped += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timeElasped / 1.5f);
            customer.color = new Color(customerTransparency.r, customerTransparency.g, customerTransparency.b, alpha);

            yield return null;
        }

        customer.color = new Color(customerTransparency.r, customerTransparency.g, customerTransparency.b, endAlpha);

        if (state) ChooseOrder();
        else gameObject.SetActive(false);
    }

    private void ChooseOrder()
    {
        int number = Random.Range(0, 3);
        customerOrder = foods[number];
        orderImage.sprite = foodImages[number];
        ordering.SetActive(true);
    }
}
