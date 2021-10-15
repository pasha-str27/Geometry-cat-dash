using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonInfo : MonoBehaviour
{
    public bool isBuyedSkin = false;
    public bool isChosenSkin = false;
    public string skinName;
    public int price = 3;

    private void Start()
    {
        if(!PlayerPrefs.HasKey(skinName + "buyed"))
            transform.GetChild(0).GetComponent<Text>().text = price.ToString() + " звезди";
    }
}
