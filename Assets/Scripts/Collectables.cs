using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables: MonoBehaviour
{
    Item itemData;

    // Start is called before the first frame update
    void Start()
    {
        itemData = GetComponent<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            string itemName = itemData.getName();
            string itemType = itemData.getType();
            float itemId = itemData.getId();
            Debug.Log(itemName + ' ' + itemType + ' ' + itemId);

            //populateInventory
            Destroy(this.gameObject);
        }
    }
}
