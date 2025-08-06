using UnityEngine;

public class Finger : MonoBehaviour
{
    public float clickAmount = 1;
    [SerializeField] float clickCooldown = 1;
    [SerializeField] Animator animator;
    float clickTimer;
    private void Start()
    {
        clickTimer = clickCooldown;
    }

    public void OnUpdate(Clicker clicker)
    {
        clickTimer -= Time.deltaTime;

        if (clickTimer <= 0)
        {
            Click(clicker);
            clickTimer = clickCooldown;
        }
    }
    void Click(Clicker clicker)
    {
        animator.SetTrigger("Click");
        clicker.Click(clickAmount);
    }
}
