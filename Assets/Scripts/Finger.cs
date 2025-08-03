using UnityEngine;

public class Finger : MonoBehaviour
{
    public float clickAmount = 1;
    [SerializeField] float clickCooldown = 1;
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
        clicker.ClickAndDamage(clickAmount);
    }
}
