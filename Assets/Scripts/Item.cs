using UnityEngine;

[System.Serializable]
public class Item: MonoBehaviour
{
    public string itemName;
    public string itemType;
    public int itemId;
    public string spriteFile;

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

    public string getSprite()
    {
        return spriteFile;
    }

    public string printObject()
    {
        return itemId.ToString() + '_' + itemType + '_' + itemName;
    }

}
