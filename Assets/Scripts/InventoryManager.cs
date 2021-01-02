using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    /// <summary> 
    /// This class handles the inventory.  There is a temporary cache that it reads from (collectable cache) and updates the inventory cache.
    /// There is a possible bug where the inventory cache may not see an item that is already populated in the inventory.  I don't think 
    /// this will be a problem however.  Any item that is recieved in the game can be forced into the temp cache.
    /// </summary>
    GameObject collectables;
    CollectableCache colCache;

    GameObject inventoryCanvas;
    GameObject inventoryObject;

    GameObject itemPrefab;
    GameObject stackBoxPrefab;

    List<Item> inventoryCache = new List<Item>();
    Item item;

    // Start is called before the first frame update
    void Start()
    {
        collectables = GameObject.Find("collectables");
        colCache = collectables.GetComponent<CollectableCache>();

        inventoryCanvas = transform.Find("inventory_canvas").gameObject;
        inventoryObject = inventoryCanvas.transform.Find("inventory").gameObject;

        itemPrefab = Resources.Load("ui_prefab\\item") as GameObject;
        stackBoxPrefab = Resources.Load("ui_prefab\\item_stack_box") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryCanvas.activeSelf)
        {
            cacheInventory();
        }
    }

    private void cacheInventory()
    {
        if (colCache.collectedCache.Count != 0)
        {
            foreach (Item i in colCache.collectedCache)
            {
                item = i;
                // hold a record of item in inventoryCache and remove the item from the temporary collectedCache. 
                // as of now the collectedCache exists to hold the collected items until the user opens their inventory and the inventory object is made active.
                populateInventory();
                inventoryCache.Add(item);
            }

            // once we unload the temp cach to inventory we will want to clear it.
            colCache.collectedCache.Clear();
        }
    }

    private void populateInventory()
    {
        bool itemExists = isDuplicate();
        bool itemAdded = false;

        foreach (Transform row in inventoryObject.transform)
        {
            if (itemAdded){ break; }
            else
            {
                foreach (Transform itemSlot in row.transform)
                {
                    if (itemExists)
                    {
                        itemAdded = stackItem(itemSlot);
                        if (itemAdded) { break; }
                    }
                    else
                    {
                        itemAdded = createNewItem(itemSlot);
                        if(itemAdded){ break; }
                    }
                }
            }
        }
    }

    private bool createNewItem(Transform itemSlot)
    {
        // populates an empty item slot with the item in the inventory cache
        bool itemAdded = false;

        if (itemSlot.childCount == 0)
        {
            Sprite inventorySprite = Resources.Load<Sprite>("sprites\\" + item.getSprite());
            string itemSlotName = item.getId() + "_" + item.getName();

            GameObject newItem = Instantiate(itemPrefab) as GameObject;
            newItem.GetComponent<Image>().sprite = inventorySprite;
            newItem.name = itemSlotName;

            newItem.transform.SetParent(itemSlot);
            newItem.transform.localScale = Vector3.one;
            newItem.transform.localPosition = Vector3.zero;

            itemAdded = true;
        }

        return itemAdded;
    }

    private bool stackItem(Transform itemSlot)
    {
        // if an item already exists in the inventory, and it is a stackable item, we will stack the item rather than take an empty slot
        bool itemAdded = false;
        string itemSlotName = item.getId() + "_" + item.getName();
        Transform populatedItem = itemSlot.Find(itemSlotName);
      
        if (item.getStackable() && populatedItem != null)
        {
            GameObject stackBox;
            Transform existingStackBox = populatedItem.Find("item_stack_box(Clone)");

            // if the stack box counter exists, edit it, otherwise instantiate a new stack box
            if (existingStackBox != null)
            {
                stackBox = existingStackBox.gameObject;
            }
            else
            {
                stackBox = Instantiate(stackBoxPrefab) as GameObject;
                stackBox.transform.SetParent(populatedItem);
                setAnchor(stackBox, new Vector2(1.0f, 0.0f), new Vector2(1.0f, 0.0f), new Vector2(0.5f, 0.5f));
                stackBox.transform.localPosition = new Vector3(35.0f, -35.0f, 0.0f);
            }
            
            // update stack count
            GameObject stackNumberBox = stackBox.transform.Find("Text").gameObject;
            int stackCount = int.Parse(stackNumberBox.GetComponent<Text>().text);
            stackCount += 1;
            stackNumberBox.GetComponent<Text>().text = stackCount.ToString();

            itemAdded = true;
        }

        return itemAdded;
    }

    private bool isDuplicate()
    {
        bool itemExists = false;
        foreach(Item i in inventoryCache)
        {
            itemExists = i.getId() == item.getId() ? true : false;
            if (itemExists) { break; }
        }

        return itemExists;
    }

    private void setAnchor(GameObject panel, Vector2 minAnchor, Vector2 maxAnchor, Vector2 pivotAnchor)
    {
        RectTransform panelRectTrans = panel.GetComponent<RectTransform>();
        panelRectTrans.anchorMin = minAnchor;
        panelRectTrans.anchorMax = maxAnchor;
        panelRectTrans.pivot = pivotAnchor;
        
    }
}
