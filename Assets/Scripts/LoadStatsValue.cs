using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadStatsValue : MonoBehaviour
{
    public string valueName;
    public int maxLevelCount = 2;

    void Start()
    {
        var text = GetComponent<Text>();

        int count = 0;
        if(valueName== "levelsComplete")
        {
            for (int i = 0; i < maxLevelCount; ++i)
                if (PlayerPrefs.HasKey("levelComplete" + i.ToString()))
                    ++count;

            text.text = count.ToString();
            return;
        }

        if (PlayerPrefs.HasKey(valueName))
            text.text = PlayerPrefs.GetInt(valueName).ToString();
    }
}
