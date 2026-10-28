using TMPro;
using UnityEngine;

public class WorkChangeTrigger : MonoBehaviour
{
    public GameDirector gd;
    public GameObject nextScene, currentScene;

    public TextMeshProUGUI timeSelected;

    public void ChangeScene()
    {
        gd.TriggerSceneChange(currentScene, nextScene);
        gd.StatPanelSwitch(false);
    }

    public void UpdateTime(float value)
    {
        timeSelected.text = value.ToString() + "H";
    }
}
