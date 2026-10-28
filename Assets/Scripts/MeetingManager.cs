using UnityEngine;

public class MeetingManager : MonoBehaviour
{
    [SerializeField] private bool meetingStarted = false;
    private Animator animator;

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
        int friendSelect = Random.Range(1, 3);
        animator.SetInteger("Friend Choice", friendSelect);
    }

    public void SelectRock()
    {
        animator.SetInteger("Player Choice", 1);
    }
    public void SelectScissor()
    {
        animator.SetInteger("Player Choice", 2);
    }
    public void SelectPaper()
    {
        animator.SetInteger("Player Choice", 3);
    }
}
