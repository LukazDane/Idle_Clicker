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
    [SerializeField] private TMP_Text manaPerSecondText;

    public BigDouble ClickPower()
    {
        BigDouble total = 1;
        for (int i = 0; i < data.clickUpgradeLevel.Count; i++)
        {
            total += UpgradesManager.instance.clickUpgradesBasePower[i] * data.clickUpgradeLevel[i];
        }
        return total;
    }
    public BigDouble ManaPerSecond()
    {
        BigDouble total = 0;
        for (int i = 0; i < data.productionUpgradeLevel.Count; i++)
        {
            total += UpgradesManager.instance.productionUpgradesBasePower[i] * data.productionUpgradeLevel[i];
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
        manaText.text = $"{data.mana:F2} Mana";
        manaClickPowerText.text = $"+{ClickPower():F2} Mana";
        manaPerSecondText.text = $"{ManaPerSecond():F2} /mps";

        data.mana += ManaPerSecond() * Time.deltaTime;

    }
    public void GenerateMana()
    {
        data.mana += ClickPower();
    }
}