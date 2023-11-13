using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int playerMoney;

    public GlobalEnum.TownNames currentTown = GlobalEnum.TownNames.Oasis;
    public Dictionary<GlobalEnum.CommoditiesNames, int> playerBag = new Dictionary<GlobalEnum.CommoditiesNames, int>();


    public void updateTown(string name)
    {
        currentTown = (GlobalEnum.TownNames)Enum.Parse(typeof(GlobalEnum.TownNames), name);
    }


    // Start is called before the first frame update
    void Start()
    {
        playerMoney = 50;
        foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
        {
            playerBag.Add(name, 0);
        }
    }





}
