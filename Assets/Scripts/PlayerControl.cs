using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    public float speed;
    public float jumpVelocity;
    public Text scoreText;
    public Text winText;
    public GameObject restartButton;

    private Rigidbody rb;
    private int score;
    private bool canJumpAgain;
    private float distToGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        updateText(score);
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    void Update()
    {
        if (IsGrounded())
        {
            canJumpAgain = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                rb.AddForce(new Vector3(0.0f, jumpVelocity, 0.0f));
            }
            else
            {
                if (canJumpAgain)
                {
                    rb.AddForce(new Vector3(0.0f, jumpVelocity, 0.0f));
                    canJumpAgain = false;
                }
            }
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            score++;
            updateText(score);
        }

        if(other.gameObject.CompareTag("OutOfBounds"))
        {
            transform.position = new Vector3(0, 1, 0);
        }

    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.15f);
    }

    void updateText(int value) 
    {
        if (value >= 12)
        {
            winText.text = "You Win!";
            restartButton.SetActive(true);
        }

        scoreText.text = "Collected: " + value.ToString() + "/12";
    }
}
