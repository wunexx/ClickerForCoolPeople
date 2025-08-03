using UnityEngine;
using System.Collections.Generic;

public class FingerManager : MonoBehaviour
{
    [HideInInspector] public List<Finger> fingers = new List<Finger>();
    [HideInInspector] public float clickAmount = 1;
    Clicker clicker;

    private void Start()
    {
        clicker = FindFirstObjectByType<Clicker>();
    }

    private void Update()
    {
        for (int i = 0; i < fingers.Count; i++)
        {
            fingers[i].OnUpdate(clicker);
        }
    }
    public void UpgradeCurrentClickAmount(float amount)
    {
        clickAmount += amount;
        for (int i = 0; i < fingers.Count; i++)
        {
            fingers[i].clickAmount = clickAmount;
        }
    }
}
