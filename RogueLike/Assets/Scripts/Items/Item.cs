using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string _name;
    public string _itemType;
    public string _description;
    public float _value;
    public int _numberOfItem;
    
    public Item(string name, string itemType, string description, float value, int numberOfItem)
    {
        _name = name;
        _itemType = itemType;
        _description = description;
        _value = value;
        _numberOfItem = numberOfItem;
    }
    public Item()
    {

    }
}
