using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : SingletonMonoBehaviour<CurrencyManager>
{
    public int CurrencyAmount => Instance.currencyAmount;
    private int currencyAmount;

    [SerializeField] private int initialCurrencyAmount = 1000;
    [SerializeField] private int maxCurrencyAmount = 99999;
    [SerializeField] private Text currencyAmountText;
    
    private void Start() 
    {
        currencyAmount = initialCurrencyAmount;
        UpdateCurrencyText();
    }

    public void SubstactCurrency(int amountToSubstract)
    {
        if(amountToSubstract > currencyAmount)
        {
            currencyAmount = 0;
            return;
        }

        currencyAmount -= amountToSubstract;
        UpdateCurrencyText();
    }

    public void AddCurrency(int amountToAdd)
    {
        if(currencyAmount + amountToAdd > maxCurrencyAmount)
        {
            currencyAmount = maxCurrencyAmount;
            return;
        }

        currencyAmount += amountToAdd;
        UpdateCurrencyText();
    }

    private void UpdateCurrencyText()
    {
        currencyAmountText.text = currencyAmount.ToString();
    }
}
