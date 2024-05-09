using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : SingletonMonoBehaviour<InventoryManager>
{
    public static Action<Item> OnItemSold;
    public static Action<ItemType> OnItemEquipped;

    [SerializeField] private Transform itemsContainer;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Button equipSubmenuButton;
    [SerializeField] private Button mainSubmenuButton;
    [SerializeField] private Text submenuText;
    private Dictionary<Item, int> itemQuantities = new Dictionary<Item, int>();
    private Dictionary<Item, Text> itemTexts = new Dictionary<Item, Text>();
    private Dictionary<Item, Button> itemButtons = new Dictionary<Item, Button>();

    private void Start() 
    {
        mainSubmenuButton.gameObject.SetActive(false);
    }

    private void OnEnable() 
    {
        ShopManager.OnItemPurchased += AddItem;
    }

    private void OnDisble() 
    {
        ShopManager.OnItemPurchased -= AddItem;
    }

    private void AddItem(Item item)
    {
        if(itemQuantities.ContainsKey(item))
        {
            itemQuantities[item]++;
            UpdateItemQuantityInUI(item, itemQuantities[item]);
        }
        else
        {
            itemQuantities.Add(item, 1);
            SetupItem(item);
        }
    }

    private void UpdateItemQuantityInUI(Item item, int newQuantity)
    {
        if(itemTexts.ContainsKey(item))
        {
            itemTexts[item].text = "x " + newQuantity;
        }
    }

    private void SetupItem(Item item)
    {
        GameObject newItem = Instantiate(itemPrefab, itemsContainer);
        newItem.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.icon;

        string itemText = "x " + itemQuantities[item];
        Text newItemText = newItem.transform.Find("ItemQuantityText").GetComponent<Text>();
        newItemText.text = itemText;
        itemTexts.Add(item, newItemText);

        Button newItemButton = newItem.GetComponent<Button>();
        itemButtons.Add(item, newItemButton);
    }

    public void GoToEquipSubmenu()
    {
        equipSubmenuButton.gameObject.SetActive(false);
        mainSubmenuButton.gameObject.SetActive(true);
        submenuText.text = "Equip";

        foreach(KeyValuePair<Item, Button> itemButton in itemButtons)
        {
            itemButton.Value.onClick.AddListener(delegate { TryToEquipItem(itemButton.Key); });
        }
    }

    public void GoToMainSubmenu()
    {
        mainSubmenuButton.gameObject.SetActive(false);
        equipSubmenuButton.gameObject.SetActive(true);
        submenuText.text = "Inventory";
        
        RemoveItemButtonsListeners();
    }

    public void RemoveItemButtonsListeners()
    {
        foreach(Button button in itemButtons.Values)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    public void EnableSelling()
    {
        foreach(KeyValuePair<Item, Button> itemButton in itemButtons)
        {
            itemButton.Value.onClick.AddListener(delegate { TryToSellItem(itemButton.Key); });
        }
    }

    public void EnableEquipping()
    {
        foreach(KeyValuePair<Item, Button> itemButton in itemButtons)
        {
            itemButton.Value.onClick.AddListener(delegate { TryToEquipItem(itemButton.Key); });
        }
    }

    private void TryToSellItem(Item item)
    {
        if(!itemQuantities.ContainsKey(item))
        {
            return;
        }

        itemQuantities[item]--;

        UpdateItemQuantityInUI(item, itemQuantities[item]);
        CurrencyManager.Instance.AddCurrency(item.price);
        OnItemSold?.Invoke(item);

        if(itemQuantities[item] == 0)
        {
            itemQuantities.Remove(item);
            itemTexts.Remove(item);
            Destroy(itemButtons[item].gameObject);
            itemButtons.Remove(item);
        }
    }

    private void TryToEquipItem(Item item)
    {
        if(item.type == ItemType.BOW || item.type == ItemType.SWORD)
        {
            OnItemEquipped?.Invoke(item.type);
        }
    }
}
