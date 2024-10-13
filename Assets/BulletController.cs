using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    float time;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right *speed;

        if (Time.time - time > 10f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
