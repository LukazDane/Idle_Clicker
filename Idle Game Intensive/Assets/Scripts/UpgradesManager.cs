using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public Controller controller;
    public Upgrades clickUpgrade;
    public string clickUpgradeName;
    public BigDouble clickUpgradeBaseCost;
    public BigDouble clickUpgradeCostMult;

    public void StartUpgradeManager()
    {
        clickUpgradeName = "Mana per charge";
        clickUpgradeBaseCost = 10;
        clickUpgradeCostMult = 1.5;
    }

    public void UpdateClickUpgradeUI()
    {
        clickUpgrade.LevelText.text = controller.data.clickUpgradeLevel.ToString();
        clickUpgrade.CostText.text = "Cost: " + Cost().ToString(format: "F2") + " Mana";
        clickUpgrade.NameText.text = "+1 " + clickUpgradeName;
    }
    public BigDouble Cost()
    {
        return clickUpgradeBaseCost * BigDouble.Pow(clickUpgradeCostMult, controller.data.clickUpgradeLevel);
    }
    public void BuyUpgrade()
    {
        if (controller.data.mana >= Cost())
        {
            controller.data.mana -= Cost();
            controller.data.clickUpgradeLevel += 1;
            // do not set upgrade to anything other than += 1 for now, using other increments could cause infinity break error  with future multipliers
        }
        UpdateClickUpgradeUI();
    }
}
