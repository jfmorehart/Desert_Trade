using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventRestrict : MonoBehaviour
{
    public int count = 0;

    public int maxi = 5;

    public BtnTrade updateRemain;

    void OnEnable()
    {
        count = 0;
    }

    public bool CheckIfMax()
    {
        if(count >= maxi)
        {
            return true;
        }
        return false;

    }

    public void returnQuant()
    {
        count--;
    }

    public void UpdateMax()
    {
        count++;
    }

}
