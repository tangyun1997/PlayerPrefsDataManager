using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public int num;

    public Item(int id, int num)
    {
        this.id = id;
        this.num = num;
    }
}

public class PlayerInfo
{
    public string name;

    public int age;
    public float atk;
    public float def;

    public int itemSize;
    public List<Item> itemList = new();

    public void Save()
    {
        PlayerPrefs.SetString("name", name);
        PlayerPrefs.SetInt("age", age);
        PlayerPrefs.SetFloat("atk", atk);
        PlayerPrefs.SetFloat("def", def);

        itemSize = itemList.Count;
        PlayerPrefs.SetInt("itemSize", itemSize);

        for (int i = 0; i < itemSize; i++)
        {
            string key1 = "item" + i + "id";
            string key2 = "item" + i + "num";

            PlayerPrefs.SetInt(key1, itemList[i].id);
            PlayerPrefs.SetInt(key2, itemList[i].num);
        }

        PlayerPrefs.Save();
    }

    public void Load()
    {
        name = PlayerPrefs.GetString("name", "defaultPlayerName");
        age = PlayerPrefs.GetInt("age", 0);
        atk = PlayerPrefs.GetFloat("atk", 1.0f);
        def = PlayerPrefs.GetFloat("def", 1.0f);

        itemSize = PlayerPrefs.GetInt("itemSize", 0);

        itemList.Clear();

        for (int i = 0; i < itemSize; i++)
        {
            string key1 = "item" + i + "id";
            string key2 = "item" + i + "num";
            
            itemList.Add(new(PlayerPrefs.GetInt(key1), PlayerPrefs.GetInt(key2)));
        }
    }

    public void PrintList()
    {
        Debug.Log(itemSize);

        for (int i = 0; i < itemSize; i++)
        {
            Debug.Log(itemList[i].id);
            Debug.Log(itemList[i].num);
        }
    }
}
