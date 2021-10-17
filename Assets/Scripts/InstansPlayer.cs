using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstansPlayer : MonoBehaviour
{
    public GameController gameController;

    void Start()
    {
        var prefab = Resources.Load("Skins/skin1") as GameObject;

        if (PlayerPrefs.HasKey("chosenSkin"))
            prefab = Resources.Load("Skins/" + PlayerPrefs.GetString("chosenSkin")) as GameObject;

        var player = Instantiate(prefab, Vector2.zero, Quaternion.identity);

        GetComponent<CameraFollow>().target = player.transform;

       // transform.SetParent(player.transform);

        player.GetComponent<PlayerController>().gameController = this.gameController;

        //StartCoroutine(levelTimer());
    }

    //IEnumerator levelTimer()
    //{
    //    yield return new WaitForSeconds(levelTime);
    //    Time.timeScale = 0;
    //}
}
