using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    GameObject collectables;
    CollectableCache colCache;

    GameObject inventoryCanvas { get { return transform.Find("inventory_canvas").gameObject; } }
    GameObject inventoryObject;

    public GameObject itemPrefab;
    GameObject newItem;

    List<Item> inventoryCache = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        collectables = GameObject.Find("collectables");
        colCache = collectables.GetComponent<CollectableCache>();

        inventoryObject = inventoryCanvas.transform.Find("inventory").gameObject;
        itemPrefab = Resources.Load("ui_prefab\\item") as GameObject;
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
                // hold a record of item in inventoryCache and remove the item from the temporary collectedCache. 
                // as of now the collectedCache exists to hold the collected items until the user opens their inventory and the inventory object is made active.
                inventoryCache.Add(i);

                // we will nee this in order to populate the ui
                string spriteFile = i.spriteFile;
                string itemName = i.itemName;
                populateInventory(spriteFile, itemName);
            }

            // once we unload the temp cach to inventory we will want to clear it.
            colCache.collectedCache.Clear();
        }
    }

    private void populateInventory(string spriteFile, string itemName)
    {
        Sprite inventorySprite = Resources.Load<Sprite>("sprites\\" + spriteFile);

        bool foundSlot = false;
        foreach(Transform row in inventoryObject.transform)
        {
            if (foundSlot)
            {
                break;
            }
            else
            {
                foreach (Transform itemSlot in row.transform)
                {
                    if (itemSlot.childCount == 0)
                    {
                        newItem = Instantiate(itemPrefab) as GameObject;
                        newItem.GetComponent<Image>().sprite = inventorySprite;
                        newItem.name = itemName;

                        newItem.transform.SetParent(itemSlot);
                        newItem.transform.localScale = Vector3.one;
                        newItem.transform.localPosition = Vector3.zero;


                        foundSlot = true;
                        break;
                    }
                }
            }
        }
    }

}
