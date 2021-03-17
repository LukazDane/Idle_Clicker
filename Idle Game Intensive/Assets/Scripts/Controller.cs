using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    public TMP_Text manaText;

    public double mana;
    public void Update()
    {
        manaText.text = mana + " Mana";
    }
    public void GenerateMana()
    {
        mana +=1;
    }
}
