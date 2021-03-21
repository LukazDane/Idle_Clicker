using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    public Data data;
    public Data data2;

    public TMP_Text manaText;

    public void Start()
    {
        data = new Data();
        data2 = new Data();
    }

    public void Update()
    {
        data.mana = 10;
        manaText.text = data.mana + " Mana\n";
        manaText.text += data2.mana + " Mana";

    }
    public void GenerateMana()
    {
        // mana +=1;
    }
}
