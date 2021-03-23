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
    public BigDouble[] clickUpgradesBasePower;

    public void StartUpgradeManager()
    {
        clickUpgradeNames = new[] { "Fire bolt +1", "Ice Knife +5", "Acid Spray +10", "Arcane Cannon +25" };
        clickUpgradeBaseCost = new BigDouble[] { 10, 50, 100, 250 };
        clickUpgradeCostMult = new BigDouble[] { 1.25, 1.35, 1.55, 2 };
        clickUpgradesBasePower = new BigDouble[] { 1, 5, 10, 25 };

        for (int i = 0; i < Controller.instance.data.clickUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel);
            upgrade.UpgradeID = i;
            clickUpgrades.Add(upgrade);
        }
        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);

        UpdateClickUpgradeUI();

        Methods.UpgradeCheck(ref Controller.instance.data.clickUpgradeLevel, 4);
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
            clickUpgrades[ID].CostText.text = $"Cost: {ClickUpgradeCost(ID):F2} Mana";
            clickUpgrades[ID].NameText.text = clickUpgradeNames[ID];

        }
    }
    public BigDouble ClickUpgradeCost(int UpgradeID)
    {
        return clickUpgradeBaseCost[UpgradeID] * BigDouble.Pow(clickUpgradeCostMult[UpgradeID], Controller.instance.data.clickUpgradeLevel[UpgradeID]);
    }
    public void BuyUpgrade(int UpgradeID)
    {
        var data = Controller.instance.data;
        if (data.mana >= ClickUpgradeCost(UpgradeID))
        {
            data.mana -= ClickUpgradeCost(UpgradeID);
            data.clickUpgradeLevel[UpgradeID] += 1;
            // do not set upgrade to anything other than += 1 for now, using other increments could cause infinity break error  with future multipliers
        }
        UpdateClickUpgradeUI(UpgradeID);
    }
}
