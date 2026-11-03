using UnityEngine;
using UnityEngine.UI;

public class DebtManager : MonoBehaviour
{
    public Animator animator;

    public void Understood()
    {
        animator.SetTrigger("Next");
    }
}
