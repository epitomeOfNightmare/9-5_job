using TMPro;
using UnityEngine;

public class MeetingManager : MonoBehaviour
{
    [SerializeField] private bool meetingStarted = false;
    private Animator animator;

    public GameDirector gd; // should have made this into a singleton

    public GameObject resultScreen, currentScene, nextScene;
    [SerializeField] private int cheapSpending, expensiveSpending;
    [SerializeField] private float cheapHappiness, expensiveHappiness, socialising;
    public TextMeshProUGUI moneyText, happinessText, socialText;

    private void OnEnable()
    {
        meetingStarted = true;
    }
    private void OnDisable()
    {
        meetingStarted = false;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = meetingStarted;
    }


    public void SelectAgree()
    {
        animator.SetTrigger("Agree");
    }
    public void SelectCheap()
    {
        animator.SetTrigger("Convince");
        FriendChoice();
    }
    public void FriendChoice()
    {
        int friendSelect = Random.Range(1, 4);
        animator.SetInteger("Friend Choice", friendSelect);
        animator.SetInteger("Player Choice", 0);
    }

    public void SelectRock()
    {
        animator.SetInteger("Player Choice", 1);
    }
    public void SelectPaper()
    {
        animator.SetInteger("Player Choice", 2);
    }
    public void SelectScissor()
    {
        animator.SetInteger("Player Choice", 3);
    }

    public void PlayerWin()
    {
        gd.MoneyModify(cheapSpending);
        gd.SocialModify(socialising);
        gd.HappinessModify(cheapHappiness);
        moneyText.text = "$ " + cheapSpending;
        happinessText.text = "++";
        socialText.text = "++";
        ShowResult();
    }
    public void FriendWin()
    {
        gd.MoneyModify(expensiveSpending);
        gd.SocialModify(socialising);
        gd.HappinessModify(expensiveHappiness);
        moneyText.text = "$ " + expensiveSpending;
        happinessText.text = "+";
        socialText.text = "++";
        ShowResult();
    }
    public void ShowResult()
    {
        resultScreen.SetActive(true);
    }


    public void ChangeScene()
    {
        gd.TriggerSceneChange(currentScene, nextScene);
        gd.StatPanelSwitch(true);
        resultScreen.SetActive(false);
    }
}
