using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OasisTown : Town
{
        //public List<CommoditiesNames> commoditiesNames;

        void Start()
        {
            commoditiesPrice = PopulatePrice(commoditiesPrice);
            foreach (var kvp in commoditiesPrice)
            {
                Debug.Log("Key: " + kvp.Key + ", Value: " + kvp.Value);
            }


        }

    private void Update()
    {

    }

}
