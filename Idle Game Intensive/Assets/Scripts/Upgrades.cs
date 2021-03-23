using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public int UpgradeID;
    public Image UpgradeButton;
    public TMP_Text LevelText;
    public TMP_Text CostText;
    public TMP_Text NameText;

    public void BuyClickUpgrade()
    {
        UpgradesManager.instance.BuyUpgrade(UpgradeID);
    }


}
