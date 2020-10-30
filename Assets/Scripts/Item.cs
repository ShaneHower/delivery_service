using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public string itemName;
    public string itemType;
    public int itemId;

    public string getName()
    {
        return itemName;
    }

    public int getId()
    {
        return itemId;
    }

    public string getType()
    {
        return itemType;
    }

}
