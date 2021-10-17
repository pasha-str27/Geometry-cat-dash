using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int levelTime = 45;
    public int time;
    public int level = 1;

    void Start()
    {
        time = 0;
        StartCoroutine(LevelTimer());
    }

    IEnumerator LevelTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            time += 1;
            print(time);
        }
    }

    public void GameOver()
    {
        PlayerPrefs.SetFloat("level" + level.ToString() + "progress", (float)time/levelTime);

        if (!PlayerPrefs.HasKey("level" + level.ToString() + "tryes"))
            PlayerPrefs.SetInt("level" + level.ToString() + "tryes", 1);
        else
            PlayerPrefs.SetInt("level" + level.ToString() + "tryes", PlayerPrefs.GetInt("level" + level.ToString() + "tryes") + 1);
    }
}
