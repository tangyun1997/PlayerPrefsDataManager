using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson1_Prac : MonoBehaviour
{
    [ContextMenu("Çå¿ÕPlayerPrefs")]
    private void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    void Start()
    {
        PlayerInfo p1 = new();

        p1.Load();

        print(p1.name);
        p1.PrintList();

        p1.name = "fxxl";
        p1.itemList.Add(new(0, 1));
        p1.itemList.Add(new(1, 5));
        p1.itemList.Add(new(2, 9));
        p1.itemList.Add(new(3, 7));

        p1.Save();
    }
}
