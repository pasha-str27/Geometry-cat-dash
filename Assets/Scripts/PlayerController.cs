using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public float force = 30;
    public float forceToFly = 0.1f;
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
    int jumpsBySession = 0;

    public Sprite jumpTexture;
    public Sprite flyTexture;

    bool isFlyingMode = false;

    bool changeAngles = false;

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
        if(isFlyingMode)
        {
            if (Input.GetMouseButtonDown(0))
                changeAngles = true;

            if (changeAngles)
                transform.eulerAngles = new Vector3(0, 0, rigidbody.velocity.y * 2);
        }

        if ((canJump || isFlyingMode) && Input.GetMouseButton(0))
        {
            if(isFlyingMode)
                rigidbody.AddForce(Vector2.up  * forceToFly, ForceMode2D.Impulse);
            else
                rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);

            if (canJump && !isFlyingMode)
            {
                canJump = false;
                jumpsCount++;
                jumpsBySession++;
            }
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

        if (!PlayerPrefs.HasKey("Jumps"))
            PlayerPrefs.SetInt("Jumps", jumpsBySession);
        else
            PlayerPrefs.SetInt("Jumps", jumpsBySession + PlayerPrefs.GetInt("Jumps"));

        if (!PlayerPrefs.HasKey("Tryes"))
            PlayerPrefs.SetInt("Tryes", 1);
        else
            PlayerPrefs.SetInt("Tryes", 1 + PlayerPrefs.GetInt("Tryes"));

        if (!PlayerPrefs.HasKey("Stars"))
            PlayerPrefs.SetInt("Stars", starsCount);
        else
            PlayerPrefs.SetInt("Stars", starsCount + PlayerPrefs.GetInt("Stars"));
        

        gameController.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("GameModeChanger"))
        {
            if (isFlyingMode)
                GetComponent<SpriteRenderer>().sprite = jumpTexture;
            else
                GetComponent<SpriteRenderer>().sprite = flyTexture;

            isFlyingMode = !isFlyingMode;
            transform.eulerAngles = Vector3.zero;
        }

        if (collision.CompareTag("Star"))
        {
            print("here");
            Destroy(collision.gameObject);
            starsCount++;
        }

        if (collision.CompareTag("LevelFinish"))
        {
            if (!PlayerPrefs.HasKey("levelComplete" + gameController.level.ToString()))
                PlayerPrefs.SetInt("levelComplete" + gameController.level.ToString(), 1);

            UpdateResults();
            SceneManager.LoadScene("Menu");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Triangle"))
        {
            PlayerPrefs.SetFloat("deadPosX" + gameController.level.ToString(), gameObject.transform.position.x);
            PlayerPrefs.SetFloat("deadPosY" + gameController.level.ToString(), gameObject.transform.position.y);

            UpdateResults();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            if (!isFlyingMode)
            {
                canJump = true;
                canMove = true;
            }
            else
            {
                PlayerPrefs.SetFloat("deadPosX" + gameController.level.ToString(), gameObject.transform.position.x);
                PlayerPrefs.SetFloat("deadPosY" + gameController.level.ToString(), gameObject.transform.position.y);

                UpdateResults();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;
            }
        }
    }
}
