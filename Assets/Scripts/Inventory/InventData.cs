using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class InventData
{
    private static GlobalEnum.CommoditiesNames currentCommodity;

    public static bool isBuy = true;


    public static bool ChangeOption(bool _isBuy)
    {
        isBuy = _isBuy;
        return isBuy;
    }

    public static void ChangeOption2(string name)
    {
        if (!Enum.TryParse(name, out currentCommodity))
        {
            Console.WriteLine("PopUp entry is wrong");
        }
    }


    public static bool updateQuant()
    {
        int amount = 1;
        if (isBuy)
        {
            if (PlayerInventory.playerMoney < (InventDisplay.currentTown.UseUpdatePriceSingle(currentCommodity) * amount))
            {
                return false;
            }
            InventDisplay.currentTown.supplyList[currentCommodity] -= amount;
            PlayerInventory.playerMoney -= (InventDisplay.currentTown.UseUpdatePriceSingle(currentCommodity) * amount);
            PlayerInventory.playerBag[currentCommodity] += amount;
        }
        else
        {
            if(PlayerInventory.playerBag[currentCommodity] <= 0)
            {
                return false;
            }
            InventDisplay.currentTown.supplyList[currentCommodity] += amount;
            PlayerInventory.playerMoney += (InventDisplay.currentTown.UseUpdatePriceSingle(currentCommodity) * amount);
            PlayerInventory.playerBag[currentCommodity] -= amount;
        }
        return true;
    }
}
