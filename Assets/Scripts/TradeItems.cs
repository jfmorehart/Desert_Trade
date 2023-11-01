using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "tradeItems", menuName = "ScriptableObjects/TradeItems", order = 1)]
public class TradeItems : ScriptableObject
{
    public string townExportFrom;
    public string commodityName;
    public int commodityBasePrice;
}
