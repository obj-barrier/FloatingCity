using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float accel;
    public float maxSpeed;

    private ScoreKeeper scoreKeeper;

    private void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector2.up);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector2.down);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right);
        }
        rb.totalForce = rb.totalForce.normalized * accel;

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    public void Die()
    {
        scoreKeeper.Lose();
        GetComponent<AudioSource>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponentInChildren<WeaponController>().gameObject.SetActive(false);
    }
}
