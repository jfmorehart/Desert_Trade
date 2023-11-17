using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventRestrict : MonoBehaviour
{
    public int max = 0;

    public int limit = 5;

    public BtnTrade updateRemain;

    void OnEnable()
    {
        max = 0;
    }

    public bool CheckIfMax()
    {
        if(max >= limit)
        {
            return true;
        }
        return false;

    }

    public void UpdateMax()
    {
        max++;
    }
    //private void OnEnable()
    //{
    //    updateRemain = GameObject.Find("_Inventory").GetComponent<BtnTrade>();

    //}
}
