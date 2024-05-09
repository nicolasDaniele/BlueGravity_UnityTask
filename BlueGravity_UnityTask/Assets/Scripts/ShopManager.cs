using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static Action<Item> OnItemPurchased;

    [SerializeField] private Item[] items;
    [SerializeField] private Transform itemsContainer;
    [SerializeField] private GameObject actionsContainer;
    
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private GameObject returnButton;
    private GameObject currentSubmenu = null;
    private Dictionary<Item, Text> itemTexts = new Dictionary<Item, Text>();
    private Dictionary<Item, Button> itemButtons = new Dictionary<Item, Button>();

    private void Start() 
    {
        foreach(Item item in items)
        {
            SetupItem(item);
        }

        HideItems();
        HideActions();
        SetReturnButtonActive(false);
    }

    private void OnEnable() 
    {
        ShopKeeperNPC.OnShopkeeperInteract += ShowActions;
        ShopKeeperNPC.OnShopkeeperEndedInteraction += HideMenu;
        InventoryManager.OnItemSold += AddItemToStock;
    }

    private void OnDisable() 
    {
        ShopKeeperNPC.OnShopkeeperInteract -= ShowActions;
        ShopKeeperNPC.OnShopkeeperEndedInteraction -= HideMenu;
        InventoryManager.OnItemSold -= AddItemToStock;
    }

    private void SetupItem(Item item)
    {
        GameObject shopItem = Instantiate(shopItemPrefab, itemsContainer);
        
        Text itemName = shopItem.transform.Find("ItemNameText").GetComponent<Text>();
        itemName.text = item.name;
        itemTexts.Add(item, itemName);

        shopItem.transform.Find("ItemPriceText").GetComponent<Text>().text = item.price.ToString();
        shopItem.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.icon;

        Button itemButton = shopItem.GetComponent<Button>(); 
        itemButton.onClick.AddListener(delegate { TryToPurchaseItem(item); });
        itemButtons.Add(item, itemButton);
    }

    private void AddItemToStock(Item item)
    {
        item.stock++;
    }

    private void HideMenu()
    {
        HideActions();
        HideItems();
        SetReturnButtonActive(false);
    }

    private void HideItems()
    {
        itemsContainer.gameObject.SetActive(false);
    }

    public void ShowItems()
    {
        HideActions();
        itemsContainer.gameObject.SetActive(true);
    }  

    public void HideActions()
    {
        actionsContainer.SetActive(false);
    }

    public void ShowActions()
    {
        HideItems();
        actionsContainer.SetActive(true);
    }

    public void TryToPurchaseItem(Item item)
    {
        if(!CanAffordItem(item.price) || item.stock <= 0)
        {
            return;
        }

        CurrencyManager.Instance.SubstactCurrency(item.price);
        OnItemPurchased?.Invoke(item);
        item.stock--;
        CheckForItemStock(item);
    }

    private bool CanAffordItem(int itemPrice)
    {
        return itemPrice <= CurrencyManager.Instance.CurrencyAmount;
    }

    private void CheckForItemStock(Item item)
    {
        if(item.stock == 0)
        {
            itemTexts[item].text = "Sold Out";
            itemButtons[item].onClick.RemoveAllListeners();
        }
    }

    public void SetReturnButtonActive(bool active)
    {
        returnButton.SetActive(active);
    }
}