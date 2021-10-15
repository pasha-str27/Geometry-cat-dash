using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstansPlayer : MonoBehaviour
{
    void Start()
    {
        var prefab = Resources.Load("Skins/skin1") as GameObject;

        if (PlayerPrefs.HasKey("chosenSkin"))
            prefab = Resources.Load("Skins/" + PlayerPrefs.GetString("chosenSkin")) as GameObject;

        var player = Instantiate(prefab, Vector2.zero, Quaternion.identity);

        transform.SetParent(player.transform);
    }
}
