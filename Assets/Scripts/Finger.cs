using UnityEngine;

public class Finger : MonoBehaviour
{
    [SerializeField] float clickAmount;
    [SerializeField] float clickCooldown;
    Clicker clicker;

    private void Start()
    {
        clicker = GameObject.FindFirstObjectByType<Clicker>();
    }

    float clickTimer;

    private void Update()
    {
        clickTimer -= Time.deltaTime;

        if (clickTimer <= 0)
        {
            Click();
            clickTimer = clickCooldown;
        }
    }
    void Click()
    {
        clicker.Click(clickAmount);
    }
}
