using System;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyBalance : MonoBehaviour
{
    [SerializeField] private CurrencyEvents currencyEvents;
    [SerializeField] private Text currencyText;

    private void OnEnable()
    {
        currencyEvents.OnCurrencyBalanceChanged += OnCurrencyBalanceChanged;
    }
    
    private void OnDisable()
    {
        currencyEvents.OnCurrencyBalanceChanged -= OnCurrencyBalanceChanged;
    }

    private void OnCurrencyBalanceChanged(int amount)
    {
        currencyText.text = amount.ToString();
    }
}
