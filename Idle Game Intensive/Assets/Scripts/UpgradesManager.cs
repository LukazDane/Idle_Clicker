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

    public List<Upgrades> productionUpgrades;
    public Upgrades productionUpgradePrefab;

    public ScrollRect clickUpgradesScroll;
    public Transform clickUpgradesPanel;

    public ScrollRect productionUpgradesScroll;
    public Transform productionUpgradesPanel;

    public string[] clickUpgradeNames;
    public string[] productionUpgradeNames;


    public BigDouble[] clickUpgradeBaseCost;
    public BigDouble[] clickUpgradeCostMult;
    public BigDouble[] clickUpgradesBasePower;

    public BigDouble[] productionUpgradeBaseCost;
    public BigDouble[] productionUpgradeCostMult;
    public BigDouble[] productionUpgradesBasePower;

    public void StartUpgradeManager()
    {
        clickUpgradeNames = new[] { "Fire bolt +1", "Ice Knife +5", "Acid Spray +10", "Arcane Cannon +25" };
        productionUpgradeNames = new[] { "Meditation +1/s", "Arcane Focus +5/s", "Portent +10/s", "Overchannel +100/s" };


        clickUpgradeBaseCost = new BigDouble[] { 10, 50, 100, 250 };
        clickUpgradeCostMult = new BigDouble[] { 1.25, 1.35, 1.55, 2 };
        clickUpgradesBasePower = new BigDouble[] { 1, 5, 10, 25 };

        productionUpgradeBaseCost = new BigDouble[] { 20, 100, 1000, 25000 };
        productionUpgradeCostMult = new BigDouble[] { 1.5, 1.75, 2, 3 };
        productionUpgradesBasePower = new BigDouble[] { 1, 5, 10, 100 };

        for (int i = 0; i < Controller.instance.data.clickUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel);
            upgrade.UpgradeID = i;
            clickUpgrades.Add(upgrade);
        }
        for (int i = 0; i < Controller.instance.data.productionUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(productionUpgradePrefab, productionUpgradesPanel);
            upgrade.UpgradeID = i;
            productionUpgrades.Add(upgrade);
        }

        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);
        productionUpgradesScroll.normalizedPosition = new Vector2(0, 0);


        UpdateUpgradeUI("click");
        UpdateUpgradeUI("production");


        Methods.UpgradeCheck(Controller.instance.data.clickUpgradeLevel, 4);
    }

    public void UpdateUpgradeUI(string type, int UpgradeID = -1)
    {
        var data = Controller.instance.data;
        switch (type)
        {
            case "click":
                if (UpgradeID == -1)
                    for (int i = 0; i < clickUpgrades.Count; i++) UpdateUi(clickUpgrades, data.clickUpgradeLevel, clickUpgradeNames, i);
                else UpdateUi(clickUpgrades, data.clickUpgradeLevel, clickUpgradeNames, UpgradeID);
                break;
            case "production":
                if (UpgradeID == -1)
                    for (int i = 0; i < productionUpgrades.Count; i++) UpdateUi(productionUpgrades, data.productionUpgradeLevel, productionUpgradeNames, i);
                else UpdateUi(productionUpgrades, data.productionUpgradeLevel, productionUpgradeNames, UpgradeID);
                break;

        }


        void UpdateUi(List<Upgrades> upgrades, List<int> upgradeLevels, string[] upgradeNames, int ID)
        {
            upgrades[ID].LevelText.text = upgradeLevels[ID].ToString();
            upgrades[ID].CostText.text = $"Cost: {UpgradeCost(type, ID):F2} Mana";
            upgrades[ID].NameText.text = upgradeNames[ID];

        }
    }
    public BigDouble UpgradeCost(string type, int UpgradeID)
    {
        var data = Controller.instance.data;
        switch (type)
        {
            case "click":
                return clickUpgradeBaseCost[UpgradeID] * BigDouble.Pow(clickUpgradeCostMult[UpgradeID], data.clickUpgradeLevel[UpgradeID]);
            case "production":
                return productionUpgradeBaseCost[UpgradeID] * BigDouble.Pow(productionUpgradeCostMult[UpgradeID], data.productionUpgradeLevel[UpgradeID]);
        }
        return 0;
    }
    public void BuyUpgrade(string type, int UpgradeID)
    {
        var data = Controller.instance.data;
        switch (type)
        {
            case "click":
                Buy(data.clickUpgradeLevel);
                break;
            case "production":
                Buy(data.productionUpgradeLevel);
                break;
        }

        void Buy(List<int> upgradeLevels)
        {

            if (data.mana >= UpgradeCost(type, UpgradeID))
            {
                data.mana -= UpgradeCost(type, UpgradeID);
                upgradeLevels[UpgradeID] += 1;
                // do not set upgradelevels to anything other than += 1 for now, using other increments could cause infinity break error  with future multipliers

                UpdateUpgradeUI(type, UpgradeID);
            }
        }
    }
}
