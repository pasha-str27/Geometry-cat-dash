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
    }
}
