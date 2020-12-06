using UnityEngine;

public class Collectables: MonoBehaviour
{
    Item item;
    CollectableCache colCache;

    // Start is called before the first frame update
    void Start()
    {
        item = GetComponent<Item>();
        colCache = GetComponentInParent<CollectableCache>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            // cache the collected item to populate inventory
            colCache.cacheCollectable(item);

            //populateInventory
            Destroy(this.gameObject);
        }
    }
}
