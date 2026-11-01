using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public int customers;
    public TextMeshProUGUI customerCounter;
    [SerializeField] private int totalActiveOrder;
    public GameObject customerOrder;
    public GameObject orderList;

    public List<GameObject> customerTag = new List<GameObject>();
    public List<CustomerOrder> orders = new List<CustomerOrder>();

    [SerializeField] private Sprite[] characterOptions;
    [SerializeField] private string[] characterNames;
    [SerializeField] private Sprite[] foodOptions;
    [SerializeField] private string[] foodNames;
    [SerializeField] private int totalOptions;
    private CustomerOrder CO;

    [SerializeField] private GameObject[] cardSlots;
    public GameObject characterCard;
    public GameObject foodCard;
    private GameObject characterSelected = null;
    private GameObject foodSelected = null;
    private string characterName;
    private string foodName;

    void Start()
    {
        CreateOrder();
    }

    private void CreateOrder()
    {
        customerCounter.text = customers.ToString();
        GameObject createdOrder;

        for (int i = 0; i < customers; i++)
        {
            createdOrder = Instantiate(customerOrder, orderList.transform);
            CO = createdOrder.GetComponent<CustomerOrder>();
            int randomNo = Random.Range(0, totalOptions);
            CO.AssignCharacter(characterOptions[randomNo], characterNames[randomNo]);
            randomNo = Random.Range(0, totalOptions);
            CO.AssignFood(foodOptions[randomNo], foodNames[randomNo]);

            customerTag.Add(createdOrder);
            orders.Add(CO);

            createdOrder.SetActive(false);
        }

        UpdateCustomers();
        CreateCards();
    }

    private void UpdateCustomers()
    {
        customerCounter.text = customers.ToString();

        for (int i = 0; i < totalActiveOrder; i++)
        {
            customerTag[i].SetActive(true);
        }
    }
    public void DecreaseCustomer(GameObject orderToRemove)
    {
        customers--;
        customerTag.Remove(orderToRemove);
        CustomerOrder customerItems = orderToRemove.GetComponent<CustomerOrder>();
        orders.Remove(customerItems);

        UpdateCustomers();
    }



    public void CreateCards()
    {
        int totalCards = cardSlots.Length;
        int CCards = totalCards/2, FCards = totalCards/2;
        GameObject card;

        List<Sprite> cSprites = new List<Sprite>();
        List<string> cNames = new List<string>();
        List<Sprite> fSprites = new List<Sprite>();
        List<string> fNames = new List<string>();
        List<CustomerOrder> tempOrders = new List<CustomerOrder>(orders);

        for ( int i = 0; i < totalCards; i++ )
        {
            int orderIndex = Random.Range(0, tempOrders.Count);
            cSprites.Add(tempOrders[orderIndex].GetCharacterImage());
            cNames.Add(tempOrders[orderIndex].GetCharacterName());
            fSprites.Add(tempOrders[orderIndex].GetFoodImage());
            fNames.Add(tempOrders[orderIndex].GetFoodName());
            tempOrders.RemoveAt(orderIndex);
        }
        
        for ( int i = 0 ; i < totalCards ; i++ )
        {
            if (CCards != 0 || FCards != 0)
            {
                if (Random.value < 0.5f)
                {
                    card = Instantiate(characterCard, cardSlots[i].transform);
                    int characterIndex = Random.Range(0, cSprites.Count);
                    CustomerCard cCard = card.GetComponent<CustomerCard>();
                    cCard.PassCharacter(cSprites[characterIndex], cNames[characterIndex]);
                    cSprites.RemoveAt(characterIndex);
                    cNames.RemoveAt(characterIndex);
                }
                else
                {
                    card = Instantiate( foodCard, cardSlots[i].transform);
                    int foodIndex = Random.Range(0, fSprites.Count);
                    FoodCard fCard = card.GetComponent<FoodCard>();
                    fCard.PassFood(fSprites[foodIndex], fNames[foodIndex]);
                    fSprites.RemoveAt(foodIndex);
                    fNames.RemoveAt(foodIndex);
                }
            }
            else if (CCards == 0)
            {
                card = Instantiate(foodCard, cardSlots[i].transform);
                int foodIndex = Random.Range(0, fSprites.Count);
                FoodCard fCard = card.GetComponent<FoodCard>();
                fCard.PassFood(fSprites[foodIndex], fNames[foodIndex]);
                fSprites.RemoveAt(foodIndex);
                fNames.RemoveAt(foodIndex);
            }
            else
            {
                card = Instantiate(characterCard, cardSlots[i].transform);
                int characterIndex = Random.Range(0, cSprites.Count);
                CustomerCard cCard = card.GetComponent<CustomerCard>();
                cCard.PassCharacter(cSprites[characterIndex], cNames[characterIndex]);
                cSprites.RemoveAt(characterIndex);
                cNames.RemoveAt(characterIndex);
            }
        }
    }

    public void ReceiveCharacter(GameObject card, string name)
    {
        characterSelected = card;
        characterName = name;

        MatchCards();
    }
    public void ReceiveFood(GameObject card, string name)
    {
        foodSelected = card;
        foodName = name;

        MatchCards();
    }
    public bool CanFlipCharacter()
    {
        if (characterSelected != null) return false;
        else return true;
    }
    public bool CanFlipFood()
    {
        if (foodSelected != null) return false;
        else return true;
    }
    private void MatchCards()
    {
        if (characterSelected == null || foodSelected == null) return;

        for (int i = 0; i < totalActiveOrder; i++)
        {
            if (orders[i].CheckRequest(characterName, foodName))
            {
                GameObject orderToDelete = customerTag[i];
                DecreaseCustomer(customerTag[i]);

                Destroy(orderToDelete);
                Destroy(foodSelected);
                Destroy(characterSelected);

                characterSelected = null;
                foodSelected = null;

                return;
            }
        }

        CustomerCard customer = characterSelected.GetComponent<CustomerCard>();
        FoodCard food = foodSelected.GetComponent<FoodCard>();
        customer.HideCard();
        food.HideCard();

        characterSelected = null;
        foodSelected = null;
    }
}
