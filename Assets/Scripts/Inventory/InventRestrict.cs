using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventRestrict : MonoBehaviour
{
    public int max;

    public int limit = 5;

    public BtnTrade updateRemain;

    public bool CheckIfMax()
    {
        if(max >= limit)
        {
            return true;
        }
        max++;
        return false;

    }
    //private void OnEnable()
    //{
    //    updateRemain = GameObject.Find("_Inventory").GetComponent<BtnTrade>();

    //}
}
