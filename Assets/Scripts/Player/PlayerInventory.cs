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

    public int playerMoney;
    public Dictionary<CommoditiesNames, int> playerBag = new Dictionary<CommoditiesNames, int>();


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
