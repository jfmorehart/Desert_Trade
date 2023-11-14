using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BtnTrade : MonoBehaviour
{
    public InventRestrict inventRestrict;
    public TextMeshProUGUI text;
    private bool __isbuy;



    public void shift(bool _isBuy)
    {
        __isbuy = _isBuy;
        InventData.ChangeOption(_isBuy);
    }

    public void tradeItemEntry(string name)
    {
        inventRestrict = GameObject.Find("panel" + name).GetComponent<InventRestrict>();
        text = GameObject.Find(name + "Left").GetComponent<TextMeshProUGUI>();
        InventData.ChangeOption2(name);
        if (__isbuy)
        {
            if (!inventRestrict.CheckIfMax())
            {
                text.text = "Remain: " + (inventRestrict.limit - inventRestrict.max);
                InventData.updateQuant();
                InventDisplay.UpdateInventoryPlayer();
            }
            else{
                //exceeds 5 what should we do
            }
        }
        else
        {
            InventData.updateQuant();
            InventDisplay.UpdateInventoryPlayer();
        }
    }
}
