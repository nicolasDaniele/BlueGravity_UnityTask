using UnityEngine;
using UnityEngine.U2D.Animation;

public class SpriteLibraryAssetUpdater : MonoBehaviour
{
    [SerializeField] private SpriteLibraryAsset defaultSpriteAsset;
    [SerializeField] private SpriteLibraryAsset archerSpriteAsset;
    [SerializeField] private SpriteLibraryAsset warriorSpriteAsset;
    private SpriteLibrary spriteLibrary;
    private ItemType currentItemType = ItemType.NONE;

    private void Start() 
    {
        spriteLibrary = GetComponent<SpriteLibrary>();    
    }

    private void OnEnable() 
    {
        InventoryManager.OnItemEquipped += UpdateSpriteLibraryAsset;
        InventoryManager.OnItemSold += OnItemSold;
    }

    private void OnDisable() 
    {
        InventoryManager.OnItemEquipped -= UpdateSpriteLibraryAsset;
        InventoryManager.OnItemSold -= OnItemSold;
    }

    private void UpdateSpriteLibraryAsset(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.BOW:
                spriteLibrary.spriteLibraryAsset = archerSpriteAsset;
                currentItemType = ItemType.BOW;
                break;
            case ItemType.SWORD:
                spriteLibrary.spriteLibraryAsset = warriorSpriteAsset;
                currentItemType = ItemType.SWORD;
                break;
        }
    }

    private void OnItemSold(Item item)
    {
        if(currentItemType == ItemType.NONE || currentItemType != item.type)
        {
            return;
        }

        ResetSpriteLibraryAsset();
    }

    private void ResetSpriteLibraryAsset()
    {
        spriteLibrary.spriteLibraryAsset = defaultSpriteAsset;
        currentItemType = ItemType.NONE;
    }
}
