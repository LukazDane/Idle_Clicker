using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    public Data data;

    public TMP_Text manaText;

    private void Start()
    {
        data = new Data();
    }

    private void Update()
    {
        manaText.text = data.mana + " Mana";

    }
    public void GenerateMana()
    {
        data.mana +=1;
    }
}
