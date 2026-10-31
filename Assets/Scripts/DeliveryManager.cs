using TMPro;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public int customers;
    public TextMeshProUGUI customerCounter;
    [SerializeField] private int totalActiveCustomers;
    private int currentActiveCustomers = 0;
    public GameObject customerOrder;
    public GameObject orderList;

    void Start()
    {
        AddCustomers();
    }

    private void AddCustomers()
    {
        customerCounter.text = customers.ToString();

        for (int i = currentActiveCustomers; i < totalActiveCustomers; i++)
        {
            Instantiate(customerOrder, orderList.transform);
            currentActiveCustomers++;
        }
    }
    public void DecreaseCustomer()
    {
        currentActiveCustomers--;
        customers--;
        AddCustomers();
    }
}
