using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BreakInfinity;
public class Data
{
    public BigDouble mana;
    public List<int> clickUpgradeLevel;
    public List<int> productionUpgradeLevel;


    public Data()
    {
        mana = 0;
        clickUpgradeLevel = new int[4].ToList();
        productionUpgradeLevel = new int[4].ToList();
    }
}
