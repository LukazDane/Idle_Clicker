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

    public BigDouble ClickPower() => 1 + data.clickUpgradeLevel;
    

    private void Start()
    {
        data = new Data();
        upgradesManager.StartUpgradeManager();
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
