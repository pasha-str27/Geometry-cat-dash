using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelInfo : MonoBehaviour
{

    public string level = "1";
    public Text levelLabel;
    public Image[] starsImages;
    public Slider levelProgress;
    public Text tryesValueLabel;
    public Text jumpsValueLabel;

    void Start()
    {
        levelLabel.text = "Уровень " + level;
        UpdateStars();
        UpdateProgress();

        int value = 0;

        if (PlayerPrefs.HasKey("level" + level + "tryes"))
            value = PlayerPrefs.GetInt("level" + level + "tryes");

        tryesValueLabel.text = value.ToString();

        value = 0;

        if (PlayerPrefs.HasKey("level" + level + "jumps"))
            value = PlayerPrefs.GetInt("level" + level + "jumps");
        jumpsValueLabel.text = value.ToString();    
    }

    void UpdateStars()
    {
        int stars = 0;
        if (PlayerPrefs.HasKey("level" + level + "stars"))
            stars = PlayerPrefs.GetInt("level" + level + "stars");

        for (int i = starsImages.Length - 1; i >= stars; --i)
            starsImages[i].sprite = null;
    }

    void UpdateProgress()
    {
        float progress = 0;
        if (PlayerPrefs.HasKey("level" + level + "progress"))
            progress = PlayerPrefs.GetFloat("level" + level + "progress");

        levelProgress.value = progress;
        levelProgress.transform.GetChild(levelProgress.transform.childCount - 1).gameObject.GetComponent<Text>().text = ((int)(progress*100)).ToString() + "%";
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level" + level.ToString());
    }
}
