using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int playerID;
    //these can be tweaked in the editor
    private float moveSpeed;
    public Vector2 moveSpeedRange;
    private float jumpSpeed;
    public Vector2 jumpSpeedRange;

    //input mapping (just set these in the editor)
    public KeyCode upKey = KeyCode.W;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.A;

    [HideInInspector]
    private bool grounded = true;
    private Rigidbody2D playerRigidbody;
    private float widthObject;
    private Shooting gun;
    private Vector2 range;

    private void Start()
    {
        gun = gameObject.GetComponent<Shooting>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        widthObject = GetComponent<Renderer>().bounds.size.x;
        range.y = GameObject.Find("Ground").transform.localScale.x / 2f;
    }

    private void Update()
    {
        Move();
        Jump();

        Vector2 direction = Vector2.left * (transform.position.x / Mathf.Abs(transform.position.x));
        gun.Shoot(direction);

        CheckOutOfScreen();
    }

    private void Jump()
    {
        float jumpSpeed = Remap(Mathf.Abs(transform.position.x), range.x, range.y, jumpSpeedRange.x, jumpSpeedRange.y);
        if (Input.GetKeyDown(upKey) && grounded)
        {
            playerRigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
            grounded = false;
        }
    }

    private void Move()
    {
        float moveSpeed = Remap(Mathf.Abs(transform.position.x), range.x, range.y, moveSpeedRange.x, moveSpeedRange.y);
        if (Input.GetKey(rightKey))
        {
            playerRigidbody.AddForce(Vector3.right * moveSpeed);
        }

        if (Input.GetKey(leftKey))
        {
            playerRigidbody.AddForce(Vector3.left * moveSpeed);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            grounded = true;
        }
    }

    void CheckOutOfScreen()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPos.x - widthObject < 0f || screenPos.x + widthObject > Screen.width)
        {
            Destroy(gameObject);
            FindObjectOfType<GameOver>().GameEnd(playerID);
        }
    }

    public float Remap(float input, float minOld, float maxOld, float minNew, float maxNew)
    {
        return (input - minOld) / (maxOld - minOld) * (maxNew - minNew) + minNew;
    }
}