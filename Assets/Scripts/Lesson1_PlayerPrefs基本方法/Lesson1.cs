using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("myAge", 25);
        PlayerPrefs.SetFloat("myHeight", 179.5f);
        PlayerPrefs.SetString("myName", "hikae");

        PlayerPrefs.Save();

        int age = PlayerPrefs.GetInt("myAge", 10);
        print(age);

        PlayerPrefs.DeleteKey("myAge");
        print(PlayerPrefs.GetInt("myAge", 10));
    }
}
