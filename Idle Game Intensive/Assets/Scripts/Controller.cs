using System;
using UnityEngine;
using TMPro;
using BreakInfinity;


public class Controller : MonoBehaviour
{
    public static Controller instance;
    private void Awake()
    {
        instance = this;
    }
    public Data data;

    public TMP_Text manaText;
    [SerializeField] private TMP_Text manaClickPowerText; 

    public BigDouble ClickPower()
    {
        BigDouble total = 1;
        for (int i = 0; i < data.clickUpgradeLevel.Count; i++)
        {
            total += UpgradesManager.instance.clickUpgradesBasePower[i] * data.clickUpgradeLevel[i];
        }
        return total;
    }
    

    private void Start()
    {
        data = new Data();
        UpgradesManager.instance.StartUpgradeManager();
    }

    private void Update()
    {
        manaText.text = data.mana + " Mana";
        manaClickPowerText.text = "+" + ClickPower() + " Mana";

    }
    public void GenerateMana()
    {
        data.mana += ClickPower();
    }
}
