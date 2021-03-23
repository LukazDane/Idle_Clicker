using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;
    private void Awake() => instance = this;

    public List<Upgrades> clickUpgrades;
    public Upgrades clickUpgradePrefab;

    public ScrollRect clickUpgradesScroll;
    public Transform clickUpgradesPanel;

    public string[] clickUpgradeNames;

    public BigDouble[] clickUpgradeBaseCost;
    public BigDouble[] clickUpgradeCostMult;
    public BIgDouble[] clickUpgradesBasePower;

    public void StartUpgradeManager()
    {
        clickUpgradeNames = new[] { "Mana Charge +1", "Mana Charge +5", "Mana Charge +10" };
        clickUpgradeBaseCost = new BigDouble[] { 10, 50, 100 };
        clickUpgradeCostMult = new BIgDouble[] { 1.25, 1.35, 1.55 };
        clickUpgradesBasePower = new BIgDouble[] { 1, 5, 10 };

        for (int i = 0; i < Controller.instance.data.clickUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel);
            upgrade.UpgradeID = i;
            clickUpgrades.Add(upgrade);
        }
        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);

        UpdateClickUpgradeUI();
    }

    public void UpdateClickUpgradeUI(int UpgradeID = -1)
    {
        var data = Controller.instance.data;
        if (UpgradeID == -1)
            for (int i = 0; i < clickUpgrades.Count; i++) UpdateUi(i);
        else UpdateUi(UpgradeID);

        void UpdateUi(int ID)
        {
            clickUpgrades[ID].LevelText.text = data.clickUpgradeLevel[ID].ToString();
            clickUpgrades[ID].CostText.text = $"Cost: {Cost():F2} Mana";
            clickUpgrades[ID].NameText.text = clickUpgradeNames[ID];

        }
    }
    public BigDouble Cost()
    {
        return clickUpgradeBaseCost * BigDouble.Pow(clickUpgradeCostMult, Controller.instance.data.clickUpgradeLevel);
    }
    public void BuyUpgrade()
    {
        var data = Controller.instance.data;
        if (data.mana >= Cost())
        {
            data.mana -= Cost();
            data.clickUpgradeLevel += 1;
            // do not set upgrade to anything other than += 1 for now, using other increments could cause infinity break error  with future multipliers
        }
        UpdateClickUpgradeUI();
    }
}
