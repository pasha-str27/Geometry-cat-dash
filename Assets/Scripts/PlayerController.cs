using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public float force = 30;
    bool canJump = true;
    public float speed = 2;
    bool canMove = false;
    int jumpsCount;
    public GameController gameController;
    public GameObject bestResultPrefab;
    float bestResultX;
    int starsCount = 0;
    public GameObject bestResultParticles;
    bool wasBestResult = false;
    bool isBestResultOnScene = false;

    void Start()
    {
        if (PlayerPrefs.HasKey("bestResult"+ gameController.level.ToString()))
        {
            bestResultX = PlayerPrefs.GetFloat("bestResult" + gameController.level.ToString());
            Instantiate(bestResultPrefab, new Vector3(bestResultX + 1, 0, 0), Quaternion.Euler(0,0,-90));
            isBestResultOnScene = true;
        }

        rigidbody = GetComponent<Rigidbody2D>();
        jumpsCount = 0;
        if (PlayerPrefs.HasKey("level" + gameController.level.ToString() + "jumps"))
            jumpsCount = PlayerPrefs.GetInt("level" + gameController.level.ToString() + "jumps");
    }

    private void FixedUpdate()
    {
        if (canMove)
            transform.Translate(Vector2.right * speed * Time.deltaTime * Time.deltaTime);

        if(isBestResultOnScene && !wasBestResult && bestResultX <= transform.position.x)
        {
            wasBestResult = true;
            Destroy(Instantiate(bestResultParticles, transform), 2);
        }
    }

    void Update()
    {
        if (canJump && Input.GetMouseButton(0))
        {
            rigidbody.AddForce(Vector2.up  * force, ForceMode2D.Impulse);
            canJump = false;
            jumpsCount++;
        }
    }

    public void UpdateResults()
    {
        string lelelStr = gameController.level.ToString();

        if (!PlayerPrefs.HasKey("level" + lelelStr + "stars") || starsCount > PlayerPrefs.GetInt("level" + lelelStr + "stars"))
            PlayerPrefs.SetInt("level" + lelelStr + "stars", starsCount);

        if (bestResultX < transform.position.x)
            PlayerPrefs.SetFloat("bestResult" + gameController.level.ToString(), transform.position.x);

        PlayerPrefs.SetInt("level" + lelelStr + "jumps", jumpsCount);
        gameController.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("LevelFinish"))
        {
            print("end");
            UpdateResults();
            SceneManager.LoadScene("Menu");
        }

        if (collision.CompareTag("Star"))
        {
            Destroy(collision.gameObject);
            starsCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
        canMove = true;
    }
}
