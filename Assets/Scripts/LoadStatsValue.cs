using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadStatsValue : MonoBehaviour
{
    public string valueName;

    void Start()
    {
        var text = GetComponent<Text>();

        if (PlayerPrefs.HasKey(valueName))
            text.text = PlayerPrefs.GetInt(valueName).ToString();

        //text.resizeTextForBestFit = false;
        //text.fontSize = gameObject.transform.parent.transform.GetChild(0).GetComponent<Text>().fontSize;
    }
}
