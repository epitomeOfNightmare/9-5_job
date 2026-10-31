using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkChangeTrigger : MonoBehaviour
{
    public GameDirector gd;
    public GameObject nextScene, currentScene;

    public TextMeshProUGUI timeSelected;
    public Slider timeSlider;

    [SerializeField] private int initialCustomers = 10;
    public DeliveryManager deliveryManager;

    public void ChangeScene()
    {
        gd.TriggerSceneChange(currentScene, nextScene);
        gd.StatPanelSwitch(false);
        SetCustomers();
    }

    public void UpdateTime(float value)
    {
        timeSelected.text = value.ToString() + "H";
    }

    private void SetCustomers()
    {
        int totalCustomers = initialCustomers + (int)(3 * timeSlider.value);
        deliveryManager.customers = totalCustomers;
    }
}
