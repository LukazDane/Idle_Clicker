using System;
using UnityEngine;
using TMPro;
using BreakInfinity;


public class Controller : MonoBehaviour
{
    public Data data;

    public TMP_Text manaText;

    private void Start()
    {
        data = new Data();
        data.mana = BigDouble.Add(data.mana, 10);
    }

    private void Update()
    {
        manaText.text = data.mana + " Mana";
        data.mana *= 100;
        print(data.mana);
    }
    public void GenerateMana()
    {
        data.mana +=1;
    }
}
