using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public enum CommoditiesNames
    {
        Dates,
        Cotton,
        Jewelry,
        Dagger,
        Gold,
        Copper,
        Myrrh,
        Silk
    }

    [Serializable]
    public enum TownNames
    {
    Oasis,
    Underground,
    Cave,
    Cultural
    }

    public int playerMoney;

    public TownNames currentTown;
    public Dictionary<CommoditiesNames, int> playerBag = new Dictionary<CommoditiesNames, int>();


    public void updateTown(string name)
    {
        currentTown = (TownNames)Enum.Parse(typeof(TownNames), name);
    }


    // Start is called before the first frame update
    void Start()
    {
        playerMoney = 50;
        foreach (CommoditiesNames name in Enum.GetValues(typeof(CommoditiesNames)))
        {
            playerBag.Add(name, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
