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

    private int moneyGained;
    private int matchTracker;
    public GameObject resultScreen, currentScene, nextScene;
    public TextMeshProUGUI moneyText, happinessText, socialText;
    public GameDirector gameDirector;

    void Start()
    {
        CreateOrder();
        moneyGained = 0;
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

        int activeOrders = Mathf.Min(totalActiveOrder, customerTag.Count);

        for (int i = 0; i < customerTag.Count; i++)
        {
            customerTag[i].SetActive(i < activeOrders);
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
        List<Sprite> cSprites = new List<Sprite>();
        List<string> cNames = new List<string>();
        List<Sprite> fSprites = new List<Sprite>();
        List<string> fNames = new List<string>();

        // Maximum of 9 orders, limited by available card slots
        int ordersToUse = Mathf.Min(9, orders.Count, cardSlots.Length / 2);

        matchTracker = 0;

        // Get character and food information
        for (int i = 0; i < ordersToUse; i++)
        {
            cSprites.Add(orders[i].GetCharacterImage());
            cNames.Add(orders[i].GetCharacterName());

            fSprites.Add(orders[i].GetFoodImage());
            fNames.Add(orders[i].GetFoodName());
        }

        int CCards = ordersToUse;
        int FCards = ordersToUse;

        int totalCards = CCards + FCards;

        for (int i = 0; i < totalCards; i++)
        {
            GameObject card;

            if (CCards > 0 && (FCards == 0 || Random.value < 0.5f))
            {
                card = Instantiate(characterCard, cardSlots[i].transform);

                int index = Random.Range(0, cSprites.Count);

                CustomerCard cCard = card.GetComponent<CustomerCard>();
                cCard.PassCharacter(cSprites[index], cNames[index], this);

                cSprites.RemoveAt(index);
                cNames.RemoveAt(index);

                CCards--;
            }
            else
            {
                card = Instantiate(foodCard, cardSlots[i].transform);

                int index = Random.Range(0, fSprites.Count);

                FoodCard fCard = card.GetComponent<FoodCard>();
                fCard.PassFood(fSprites[index], fNames[index], this);

                fSprites.RemoveAt(index);
                fNames.RemoveAt(index);

                FCards--;
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

        int visibleOrders = Mathf.Min(totalActiveOrder, orders.Count);

        for (int i = 0; i < visibleOrders; i++)
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

                matchTracker++;
                moneyGained += 15;
                CheckProgress();

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

    private void CheckProgress()
    {
        if (matchTracker == 9)
        {
            CreateCards();
            happinessText.text += "-";
            gameDirector.HappinessModify(0.1f);
        }
        else if (customerTag.Count == 0)
        {
            resultScreen.SetActive(true);
            moneyText.text = "$ " + moneyGained.ToString();
            socialText.text = "-";
        }
    }

    public void ChangeScene()
    {
        gameDirector.MoneyModify(moneyGained);
        gameDirector.SocialModify(0.1f);
        gameDirector.TriggerSceneChange(currentScene, nextScene);
        gameDirector.StatPanelSwitch(true);
        resultScreen.SetActive(false);
    }
}
