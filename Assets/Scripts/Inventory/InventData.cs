using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class InventData
{
    private static GlobalEnum.CommoditiesNames currentCommodity;
    public static Slider slider;
    public static bool isBuy = true;
    //private static InventDisplay inventDisplay;

    //private PlayerInventory playerInventory;
    // Start is called before the first frame update

    public static void ChangeOption(bool _isBuy)
    {
        isBuy = _isBuy;
    }
    public static void ChangeOption2(string name)
    {
        if (!Enum.TryParse(name, out currentCommodity))
        {
            Console.WriteLine("PopUp entry is wrong");
        }
    }

    public static int sliderChange()
    {
        slider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        return (int)slider.value;
    }


    public static void updateQuant()
    {
        int amount = sliderChange();
        if (isBuy)
        {
            InventDisplay.currentTown.supplyList[currentCommodity] -= amount;
            PlayerInventory.playerMoney -= (InventDisplay.currentTown.UseUpdatePriceSingle(currentCommodity) * amount);
            PlayerInventory.playerBag[currentCommodity] += amount;
        }
        else
        {
            InventDisplay.currentTown.supplyList[currentCommodity] += amount;
            PlayerInventory.playerMoney += (InventDisplay.currentTown.UseUpdatePriceSingle(currentCommodity) * amount);
            PlayerInventory.playerBag[currentCommodity] -= amount;
        }
    }
}
