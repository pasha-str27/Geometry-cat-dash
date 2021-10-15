using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsMenuController : MonoBehaviour
{
    public int levelsCount;
    public GameObject ForwardButton;
    public GameObject BackwardButton;
    public GameObject currentLevelInfo;

    int currentLevel = 1;

    void Start()
    {
        
    }
    
    public void ButtonClick(int i)
    {
        currentLevel += i;

        BackwardButton.SetActive(true);
        ForwardButton.SetActive(true);

        var nextLevelInfo = Resources.Load("LevelsInfo/Level"+ currentLevel.ToString()+"Info") as GameObject;

        var nextLevelInfoGO = Instantiate(nextLevelInfo, transform);
        Destroy(currentLevelInfo);
        currentLevelInfo = nextLevelInfoGO;
        if (currentLevel == 1)
            BackwardButton.SetActive(false);

        if (currentLevel == levelsCount)
            ForwardButton.SetActive(false);
    }
}
