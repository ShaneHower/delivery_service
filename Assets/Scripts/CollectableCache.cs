using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCache : MonoBehaviour
{
    public List<Item> collectedCache = new List<Item>();

    // Update is called once per frame
    void Update()
    {

    }

    public void cacheCollectable(Item item)
    {
        collectedCache.Add(item);
    }
}
