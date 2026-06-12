using System;
using UnityEngine;
using UnityEngine.UI;

public class ColletiveChargeUI : MonoBehaviour
{
    [SerializeField] private Image wallet;

    private void Start()
    {
        wallet.gameObject.SetActive(false);
    }

    public void ActiveUI(bool active)
    {
        wallet.gameObject.SetActive(active);
    }
}
