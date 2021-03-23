using System;
using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
public class Data
{
    public BigDouble mana;
    public List<BigDouble> clickUpgradeLevel;
    public Data()
    {
        mana = 0;
        clickUpgradeLevel = Methods.CreateList<BigDouble>(capacity: 4);
    }
}
