using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInventory
{
    public static int playerMoney = 50;

    public static GlobalEnum.TownNames currentTown = GlobalEnum.TownNames.Oasis;
    public static Dictionary<GlobalEnum.CommoditiesNames, int> playerBag = new Dictionary<GlobalEnum.CommoditiesNames, int>();


    public static void updateTown(string name)
    {
        currentTown = (GlobalEnum.TownNames)Enum.Parse(typeof(GlobalEnum.TownNames), name);
    }

    public static void PopulateBag()
    {
        foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
        {
            playerBag.Add(name, 0);
        }
    }

}
