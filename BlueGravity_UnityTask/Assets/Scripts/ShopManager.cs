using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct Item
{
    public string itemName;
    public int itemPrice;
}

public class ShopManager : MonoBehaviour
{
    public static Action<Item> OnItemPurchased;

    [SerializeField] private Item[] items;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject shopItemPrefab;

    private void Start() 
    {
        foreach(Item item in items)
        {
            SetupItem(item);
        }

        HideItems();
    }

    private void OnEnable() 
    {
        TestInteractableNPC.OnShopkeeperInteract += ShowItems;
    }

    private void OnDisable() 
    {
        TestInteractableNPC.OnShopkeeperInteract -= ShowItems;
    }

    private void SetupItem(Item item)
    {
        GameObject shopItem = Instantiate(shopItemPrefab, container);
        
        shopItem.transform.Find("ItemNameText").GetComponent<Text>().text = item.itemName;
        shopItem.transform.Find("ItemPriceText").GetComponent<Text>().text = item.itemPrice.ToString();

        shopItem.GetComponent<Button>().onClick.AddListener(delegate { TryToPurchaseItem(item); });
    }

    private void HideItems()
    {
        container.gameObject.SetActive(false);
    }

    private void ShowItems()
    {
        container.gameObject.SetActive(true);
    }  

    public void TryToPurchaseItem(Item item)
    {
        if(!CanAffordItem(item.itemPrice))
        {
            return;
        }

        CurrencyManager.Instance.SubstactCurrency(item.itemPrice);
        OnItemPurchased?.Invoke(item);
    }

    private bool CanAffordItem(int itemPrice)
    {
        return itemPrice <= CurrencyManager.Instance.CurrencyAmount;
    }
}
