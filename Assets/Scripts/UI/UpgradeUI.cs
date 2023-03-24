using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeUI : MonoBehaviour
{
    public static UpgradeUI instance;

    [Header("Components")]
    [SerializeField] PlayerData playerData;
    [SerializeField] Image[] masks;
    [SerializeField] Image expBar;
    [SerializeField] Text levelText;
    [SerializeField] Text moneyText;

    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        
        Upgrades.coin = 10000;
        Upgrades.exp = 1000;
        Upgrades.instance.ResetPlayerShopping();
        Upgrades.instance.ShiZaiXiaWudiLa();
        UpdateUpgradeUI();
    }

    private void OnEnable()
    {
        
    }

    public void UpdateUpgradeUI()
    {
        // set mask
        for (int id = 0; id < Upgrades.instance.upgrades.Length; ++id)
        {
            if(Upgrades.instance.GetUpgradeStat(id) != 0)
            {
                masks[id].gameObject.SetActive(false);
            }
            else
            {
                masks[id].gameObject.SetActive(true);
            }
        }
        // set top bar exp
        float expProg = Upgrades.exp;
        int level = 1;
        if(expProg >= playerData.level1exp)
        {
            expProg -= playerData.level1exp;
            ++level;
            if(expProg >= playerData.level2exp)
            {
                expProg -= playerData.level2exp;
                ++level;
                expProg /= playerData.level3exp;
            }
            else
            {
                expProg /= playerData.level2exp;
            }
        }
        else
        {
            expProg /= playerData.level1exp;
        }
        expBar.fillAmount = expProg;
        levelText.text = level.ToString();
        // set topbar coin
        moneyText.text = Upgrades.coin.ToString();
    }

    public void buy(int id)
    {
        if(Upgrades.instance.buy(id))
        {
            //masks[id].gameObject.SetActive(false);
            UpdateUpgradeUI();
        }
    }
}
