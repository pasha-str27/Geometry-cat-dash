using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int levelTime = 45;
    public int time;
    public int level = 1;
    public GameObject deadPlace;

    void Start()
    {
        time = 0;
        StartCoroutine(LevelTimer());

        if(PlayerPrefs.HasKey("deadPosX"+level.ToString()))
        {
            Vector2 deadPos;
            deadPos.x = PlayerPrefs.GetFloat("deadPosX" + level.ToString());
            deadPos.y = PlayerPrefs.GetFloat("deadPosY" + level.ToString()) - 1;
            Instantiate(deadPlace, deadPos, Quaternion.identity);
        }
    }

    IEnumerator LevelTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            time += 1;
        }
    }

    public void GameOver()
    {
        if(!PlayerPrefs.HasKey("level" + level.ToString() + "progress") ||
            PlayerPrefs.GetFloat("level" + level.ToString() + "progress") < (float)time / levelTime)
                PlayerPrefs.SetFloat("level" + level.ToString() + "progress", (float)time/levelTime);

        if (!PlayerPrefs.HasKey("level" + level.ToString() + "tryes"))
            PlayerPrefs.SetInt("level" + level.ToString() + "tryes", 1);
        else
            PlayerPrefs.SetInt("level" + level.ToString() + "tryes", PlayerPrefs.GetInt("level" + level.ToString() + "tryes") + 1);
    }
}
