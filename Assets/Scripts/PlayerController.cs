using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public float force = 30;
    bool canJump = true;
    public float speed = 2;
    bool canMove = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(canMove)
            transform.Translate(Vector2.right * speed * Time.deltaTime * Time.deltaTime);

        if (canJump && Input.GetMouseButton(0))
        {
            rigidbody.AddForce(Vector2.up  * force, ForceMode2D.Impulse);
            canJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
        canMove = true;
    }
}
