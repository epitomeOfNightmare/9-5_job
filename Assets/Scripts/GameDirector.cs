using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public GameObject statsPanel;
    public TextMeshProUGUI moneyAmount;
    public Slider happiness, socialisation;
    [SerializeField] private float stat_Happy, stat_Social;
    [SerializeField] private int stat_Money;

    public GameObject fader;
    [SerializeField] private float fadeDuration = 2.5f;

    private void Start()
    {
        moneyAmount.text = "$ " + stat_Money.ToString();
        stat_Happy = happiness.value;
        stat_Social = socialisation.value;
    }

    public void StatPanelSwitch(bool state)
    {
        statsPanel.SetActive(state);
    }

    public void TriggerSceneChange(GameObject currentScene,  GameObject nextScene)
    {
        StartCoroutine(SwitchScene(currentScene, nextScene));
    }
    public IEnumerator SwitchScene(GameObject currentScene, GameObject nextScene)
    {
        fader.SetActive(true);
        Image fadeImage = fader.GetComponent<Image>();
        Color fadeColor = fadeImage.color;
        float timeElasped = 0f;

        while (timeElasped < fadeDuration)
        { 
            timeElasped += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, timeElasped / fadeDuration);
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);

            yield return null;
        }

        timeElasped = 0f;
        fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1);
        currentScene.SetActive(false);
        nextScene.SetActive(true);

        while (timeElasped < fadeDuration)
        {
            timeElasped += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, timeElasped / fadeDuration);
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);

            yield return null;
        }

        fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0);
        fader.SetActive(false);
    }
}
