using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RightChecker : MonoBehaviour
{
    public PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Floor"))
        {
            PlayerPrefs.SetFloat("deadPosX" + player.gameController.level.ToString(), player.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("deadPosY" + player.gameController.level.ToString(), player.gameObject.transform.position.y);

            player.UpdateResults();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
