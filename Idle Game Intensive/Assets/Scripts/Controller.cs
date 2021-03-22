using System;
using UnityEngine;
using TMPro;
using BreakInfinity;


public class Controller : MonoBehaviour
{
    public UpgradesManager upgradesManager;
    public Data data;

    public TMP_Text manaText;
    [SerializeField] private TMP_Text ManaClickPowerText;

    public BigDouble ClickPower() => 1 + data.clickUpgradeLevel;
    

    private void Start()
    {
        data = new Data();
        data.mana = 1;
        upgradesManager.StartUpgraddeManager();
    }

    private void Update()
    {
        manaText.text = data.mana + " Mana";
        ManaClickPowerText.text = "+" + ClickPower() + " Mana";

    }
    public void GenerateMana()
    {
        data.mana += ClickPower();
    }
}
