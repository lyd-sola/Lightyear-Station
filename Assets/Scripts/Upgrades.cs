using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public static Upgrades instance;

    [SerializeField] PlayerData playerData;

    public string[] upgrades = new string[] {"TanSheCang", "HuDun", "ChaoJiQiu",
                                      "JiSuXiaZhui", "ZaiShengHuDun", "ErDuanTiao",
                                      "PenQiBeiBao", "ZhongJiHuJu", "XinShiDeJieFang"};
    public int[] prices = new int[] {100, 400, 300,
                              500, 600, 1000,
                              1200, 500, 500};
    public static int coin
    {
        get
        {
            return PlayerPrefs.GetInt("coin");
        }
        set
        {
            PlayerPrefs.SetInt("coin", value);
        }
    }

    public static int exp
    {
        get
        {
            return PlayerPrefs.GetInt("exp");
        }
        set
        {
            PlayerPrefs.SetInt("exp", value);
        }
    }


    private void Awake()
    {
        instance = this;
    }

    public int GetLevel()
    {
        int expProg = exp;
        int level = 1;
        if (expProg >= playerData.level1exp)
        {
            expProg -= playerData.level1exp;
            ++level;
            if (expProg >= playerData.level2exp)
            {
                expProg -= playerData.level2exp;
                ++level;
            }
        }
        return level;
    }

    public int GetUpgradeStat(int id)
    {
        return PlayerPrefs.GetInt(upgrades[id]);
    }

    public void SetUpgradeStat(int id, int val)
    {
        PlayerPrefs.SetInt(upgrades[id], val);
    }

    public int GetUpgradeStat(string name)
    {
        return PlayerPrefs.GetInt(name);
    }

    public void SetUpgradeStat(string name, int val)
    {
        PlayerPrefs.SetInt(name, val);
    }


    public bool buy(int id)
    {
        if (coin >= prices[id] && GetUpgradeStat(id) == 0 && id / 3 + 1 <= GetLevel())
        {
            coin = coin - prices[id];
            SetUpgradeStat(id, 1);
            return true;
        }
        return false;
    }

    public void ResetPlayerShopping()
    {
        coin = 0;
        exp = 0;
        foreach (var str in upgrades)
        {
            SetUpgradeStat(str, 0);
        }
    }

    public void ShiZaiXiaWudiLa()
    {
        exp = playerData.level1exp + playerData.level2exp + playerData.level3exp / 2;
        //exp = playerData.level1exp + 100;
        coin = 99999;
    }

    public void AllUpgrades()
    {
        foreach (var str in upgrades)
        {
            SetUpgradeStat(str, 1);
        }
    }
}
