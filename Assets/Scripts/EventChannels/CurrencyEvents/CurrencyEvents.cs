using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "CurrencyChannel", menuName = "TowerDefense/Channels/Currency", order = 0)]
public class CurrencyEvents : ScriptableObject
{
    public UnityAction<int> OnCurrencyBalanceChanged = delegate { };
    public void TriggerCurrencyChanged(int currencyAmount) => OnCurrencyBalanceChanged?.Invoke(currencyAmount);
}
