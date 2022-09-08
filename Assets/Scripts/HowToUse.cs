using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToUse : MonoBehaviour
{
    public bool isDebug;
    public bool isClearPlayerPrefs;

    private Item item1, item2, item3, item4;
    private PlayerInfo player1, player2;

    // Start is called before the first frame update
    void Start()
    {
        DataManager.Instance.debugEnabled = isDebug;

        if (isClearPlayerPrefs)
            PlayerPrefs.DeleteAll();

        LoadData();
        Game();
        SaveData();
    }

    private void LoadData()
    {
        item1 = DataManager.Instance.Load<Item>("item1"); print(item1);
        item2 = DataManager.Instance.Load<Item>("item2"); print(item2);
        item3 = DataManager.Instance.Load<Item>("item3"); print(item3);
        item4 = DataManager.Instance.Load<Item>("item4"); print(item4);

        player1 = DataManager.Instance.Load<PlayerInfo>("player1"); print(player1);
        player2 = DataManager.Instance.Load<PlayerInfo>("player2"); print(player2);
    }

    //模拟游戏中的数据变化
    private void Game()
    {
        item1.ChangeValue();
        item2.ChangeValue();
        item3.ChangeValue();
        item4.ChangeValue();
        player1.ChangeValue();
        player2.ChangeValue();
    }

    private void SaveData()
    {
        DataManager.Instance.Save("item1", item1);
        DataManager.Instance.Save("item2", item2);
        DataManager.Instance.Save("item3", item3);
        DataManager.Instance.Save("item4", item4);

        DataManager.Instance.Save("player1", player1);
        DataManager.Instance.Save("player2", player2);
    }
}
